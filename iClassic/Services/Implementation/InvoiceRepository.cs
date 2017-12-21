using iClassic.Models;
using iClassic.Services.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.UI.WebControls;
using iClassic.Helper;

namespace iClassic.Services.Implementation
{
    public class InvoiceRepository : GenericRepository<Invoice>, IDisposable
    {
        private iClassicEntities context;
        private CustomerRepository _customerRepository;
        public InvoiceRepository(iClassicEntities entities) : base(entities)
        {
            context = entities;
            _customerRepository = new CustomerRepository(entities);
        }

        public Invoice GetById(int id)
        {
            return FirstOrDefault(m => m.Id == id);
        }

        public async Task<Invoice> GetByIdAsync(int id)
        {
            return await FirstOrDefaultAsync(m => m.Id == id);
        }

        public IQueryable<Invoice> Search(InvoiceSearch model)
        {
            var list = Where(m => m.BranchId == model.BranchId);

            if (model.IsSapPhaiTra.HasValue && model.IsSapPhaiTra.Value)
            {
                var tomorrow = DateTime.Now.Date.AddDays(1);
                return list.Where(m => m.Status != (byte)TicketStatus.DaTraChoKhach).AsEnumerable().Where(m => m.NgayTra.Date <= tomorrow).OrderByDescending(m => m.Id).AsQueryable();
            }

            if (model.IsDenHanThu.HasValue && model.IsDenHanThu.Value)
            {
                var tomorrow = DateTime.Now.Date.AddDays(1);
                return list.Where(m => m.Status != (byte)TicketStatus.DaTraChoKhach).AsEnumerable().Where(m => m.NgayThu.HasValue && m.NgayThu.Value.Date <= tomorrow).AsQueryable();
            }

            if (model.StatusVai.HasValue)
            {
                return list.Where(m => m.PhieuSanXuat.Any(n => n.VaiType == (byte)VaiTypes.KhongCoSan
                && n.HasVai == model.StatusVai.Value)).OrderByDescending(m => m.Id);
            }

            if (!string.IsNullOrWhiteSpace(model.SearchText))
            {
                model.SearchText = model.SearchText.ToUpper();
                list = list.Where(m =>
                        m.Customer.TenKH.ToUpper().Contains(model.SearchText) ||
                        m.Customer.SDT.ToUpper().Contains(model.SearchText) ||
                        m.Code.ToUpper().Contains(model.SearchText));
            }

            if (model.Status.HasValue)
            {
                list = list.Where(m => m.Status == model.Status);
            }

            if (model.FromDate.HasValue)
            {
                list = list.Where(m => model.FromDate <= m.NgayThu ||
                                       model.FromDate <= m.NgayTra
                );
            }
            if (model.ToDate.HasValue)
            {
                list = list.Where(m => m.NgayThu <= model.ToDate ||
                                       m.NgayTra <= model.ToDate
                );
            }

            var sortNameUpper = !string.IsNullOrEmpty(model.SortName) ? model.SortName.ToUpper() : "";
            if (model.SortOrder == SortDirection.Ascending)
            {
                switch (sortNameUpper)
                {
                    case "TOTAL":
                        list = list.OrderBy(m => m.Total);
                        break;
                    case "DATCOC":
                        list = list.OrderBy(m => m.DatCoc);
                        break;
                    case "CustomerId":
                        list = list.OrderBy(m => m.Customer.TenKH);
                        break;
                    case "STATUS":
                        list = list.OrderBy(m => m.Status);
                        break;
                    case "NGAYTHU":
                        list = list.OrderBy(m => m.NgayThu);
                        break;
                    case "NGAYTRA":
                        list = list.OrderBy(m => m.NgayTra);
                        break;
                    default:
                        list = list.OrderBy(m => m.Created);
                        break;
                }
            }
            else
            {
                switch (sortNameUpper)
                {
                    case "TOTAL":
                        list = list.OrderByDescending(m => m.Total);
                        break;
                    case "DATCOC":
                        list = list.OrderByDescending(m => m.DatCoc);
                        break;
                    case "CustomerId":
                        list = list.OrderByDescending(m => m.Customer.TenKH);
                        break;
                    case "STATUS":
                        list = list.OrderByDescending(m => m.Status);
                        break;
                    case "NGAYTHU":
                        list = list.OrderByDescending(m => m.NgayThu);
                        break;
                    case "NGAYTRA":
                        list = list.OrderByDescending(m => m.NgayTra);
                        break;
                    default:
                        list = list.OrderByDescending(m => m.Created);
                        break;
                }
            }
            return list;
        }

        public IQueryable<Invoice> GetByDateRange(int branchId, DateTime? startDate, DateTime? endDate)
        {
            return Where(m => m.BranchId == branchId && (!startDate.HasValue || startDate <= m.NgayTra) && (!endDate.HasValue || m.NgayTra <= endDate));
        }

        public int Count(int branchId, TicketStatus status)
        {
            var stt = (byte)status;
            return Where(m => m.BranchId == branchId && m.Status == stt).Count();
        }

        public int CountChuaMuaVai(int branchId)
        {
            return Where(m => m.BranchId == branchId && m.Status != (byte)TicketStatus.DaTraChoKhach &&
                        m.Status != (byte)TicketStatus.DaXuLy &&  
                        m.PhieuSanXuat.Any(n => n.MaVaiId.HasValue && n.VaiType == (byte)VaiTypes.KhongCoSan
                                                && !n.HasVai
                        )).Count();
        }

        public override void Insert(Invoice model)
        {
            model.Created = DateTime.Now;
            model.ModifiedDate = DateTime.Now;
            model.ModifiedBy = model.CreateBy;
            model.NgayThu = model.PhieuSanXuat.Any(m => context.ProductType.Any(n => n.IsFitting && n.Id == m.ProductTypeId)) ? model.NgayThu : null;

            var memberCard = _customerRepository.GetMemberCard(model.CustomerId, model.BranchId);
            if (memberCard.Id > 0)
                model.MemberCardId = memberCard.Id;

            foreach (var item in model.PhieuSanXuat)
            {
                item.HasVai = false;

                var tienCong = context.ProductType.FirstOrDefault(m => m.Id == item.ProductTypeId).Price;
                switch ((VaiTypes)item.VaiType)
                {
                    case VaiTypes.KhachMangVaiDen:
                        item.DonGia = tienCong;
                        break;
                    case VaiTypes.KhongCoSan:
                        var tienVai = context.ProductTypeLoaiVai.FirstOrDefault(m => m.MavaiId == item.MaVaiId && m.ProductTypeId == item.ProductTypeId).Price ?? 0;
                        item.DonGia = tienVai;
                        break;
                    case VaiTypes.VaiMauCuaHang:
                        item.DonGia = (item.GiaVaiMau ?? 0);
                        break;
                }

                if (memberCard.Id > 0)
                {
                    var productMemberCard = memberCard.ProductMemberCard.FirstOrDefault(m => m.ProductId == item.ProductTypeId);
                    if (productMemberCard != null)
                    {
                        item.DonGia = item.DonGia - (item.DonGia * productMemberCard.Discount / 100);
                    }
                }
            }

            foreach (var item in model.PhieuSua)
            {                
                item.Status = (int)TicketStatus.ChuaXuLy;
                if (item.Type == (byte)PhieuSuaType.BaoHanh)
                {
                    item.SoTien = 0;                    
                }
                else
                {
                    item.ProblemBy = null; 
                }
                model.PhieuSua.Add(item);
            }           
            base.Insert(model);
        }

        public void ChangeStatus(Invoice model, int status)
        {
            if (model.Status == (byte)TicketStatus.ChuaXuLy && status == (byte)TicketStatus.DangXuLy)
            {
                model.Status = (byte)TicketStatus.DangXuLy;
                model.ModifiedDate = DateTime.Now;
            }

            if (model.Status == (byte)TicketStatus.DangXuLy && status == (byte)TicketStatus.DaXuLy)
            {
                model.Status = (byte)TicketStatus.DaXuLy;
                model.ModifiedDate = DateTime.Now;
            }

            if (model.Status == (byte)TicketStatus.DaXuLy && status == (byte)TicketStatus.DaTraChoKhach)
            {
                model.Status = (byte)TicketStatus.DaTraChoKhach;
                model.ModifiedDate = DateTime.Now;
            }
        }

        public override void Update(Invoice model)
        {
            var obj = GetById(model.Id);
            obj.Total = model.Total;
            obj.DatCoc = model.DatCoc;
            obj.CustomerId = model.CustomerId;
            obj.ChietKhau = model.ChietKhau;
            obj.ChietKhauType = model.ChietKhauType;
            model.ModifiedBy = model.ModifiedBy;
            model.ModifiedDate = DateTime.Now;
            obj.Status = model.Status;
            obj.NgayTra = model.NgayTra;
            obj.BranchId = model.BranchId;

            #region Phiếu sản xuất
            var listNew = model.PhieuSanXuat.Where(m => !obj.PhieuSanXuat.Any(n => n.Id == m.Id));
            var lisUpdate = model.PhieuSanXuat.Where(m => obj.PhieuSanXuat.Any(n => n.Id == m.Id));
            var listRemove = obj.PhieuSanXuat.Where(m => !model.PhieuSanXuat.Any(n => n.Id == m.Id));

            var memberCard = _customerRepository.GetMemberCard(model.CustomerId, model.BranchId);
            if (memberCard.Id > 0)
                obj.MemberCardId = memberCard.Id;

            foreach (var item in listNew)
            {
                item.HasVai = false;

                var tienCong = context.ProductType.FirstOrDefault(m => m.Id == item.ProductTypeId).Price;
                switch ((VaiTypes)item.VaiType)
                {
                    case VaiTypes.KhachMangVaiDen:
                        item.DonGia = tienCong;
                        break;
                    case VaiTypes.KhongCoSan:
                        var tienVai = context.ProductTypeLoaiVai.FirstOrDefault(m => m.MavaiId == item.MaVaiId && m.ProductTypeId == item.ProductTypeId).Price ?? 0;
                        item.DonGia = tienVai;
                        break;
                    case VaiTypes.VaiMauCuaHang:
                        item.DonGia = (item.GiaVaiMau ?? 0);
                        break;
                }

                if (memberCard.Id > 0)
                {
                    var productMemberCard = memberCard.ProductMemberCard.FirstOrDefault(m => m.ProductId == item.ProductTypeId);
                    if (productMemberCard != null)
                    {
                        item.DonGia = item.DonGia - (item.DonGia * productMemberCard.Discount / 100);
                    }
                }
                obj.PhieuSanXuat.Add(item);
            }

            lisUpdate.ToList().ForEach(t =>
            {
                var objForUpdate = obj.PhieuSanXuat.FirstOrDefault(m => m.Id == t.Id);
                objForUpdate.MaVaiId = t.MaVaiId;
                objForUpdate.ProductTypeId = t.ProductTypeId;
                objForUpdate.SoLuong = t.SoLuong;
                objForUpdate.TenSanPham = t.TenSanPham;
                objForUpdate.DangNguoi = t.DangNguoi;
                objForUpdate.Status = t.Status;
                objForUpdate.NgayThu = t.NgayThu;
                objForUpdate.ThoDoId = t.ThoDoId;
                objForUpdate.ThoCatId = t.ThoCatId;
                objForUpdate.ThoMayId = t.ThoMayId;
                objForUpdate.HasVai = false;
                objForUpdate.VaiType = t.VaiType;

                var tienCong = context.ProductType.FirstOrDefault(m => m.Id == t.ProductTypeId).Price;
                switch ((VaiTypes)t.VaiType)
                {
                    case VaiTypes.KhachMangVaiDen:
                        objForUpdate.DonGia = tienCong;
                        break;
                    case VaiTypes.KhongCoSan:
                        var tienVai = context.ProductTypeLoaiVai.FirstOrDefault(m => m.MavaiId == t.MaVaiId && m.ProductTypeId == t.ProductTypeId).Price ?? 0;
                        objForUpdate.DonGia = tienVai;
                        objForUpdate.HasVai = t.HasVai;
                        break;
                    case VaiTypes.VaiMauCuaHang:
                        objForUpdate.DonGia = (t.GiaVaiMau ?? 0);
                        objForUpdate.GiaVaiMau = t.GiaVaiMau;
                        break;
                }
                if (memberCard.Id > 0)
                {
                    var productMemberCard = memberCard.ProductMemberCard.FirstOrDefault(m => m.ProductId == t.ProductTypeId);
                    if (productMemberCard != null)
                    {
                        objForUpdate.DonGia = objForUpdate.DonGia - (objForUpdate.DonGia * productMemberCard.Discount / 100);
                    }
                }
            });
            context.PhieuSanXuat.RemoveRange(listRemove);
            #endregion

            #region Phiếu Sửa
            var listNewPhieusua = model.PhieuSua.Where(m => !obj.PhieuSua.Any(n => n.Id == m.Id));
            var lisUpdatePhieusua = model.PhieuSua.Where(m => obj.PhieuSua.Any(n => n.Id == m.Id));
            var listRemovePhieusua = obj.PhieuSua.Where(m => !model.PhieuSua.Any(n => n.Id == m.Id));

            foreach (var item in listNewPhieusua)
            {                
                item.Status = (int)TicketStatus.ChuaXuLy;
                if (item.Type == (byte)PhieuSuaType.BaoHanh)
                {
                    item.SoTien = 0;                    
                }
                else
                {
                    item.ProblemBy = null; 
                }
                obj.PhieuSua.Add(item);
            }           
            lisUpdatePhieusua.ToList().ForEach(t =>
            {
                var objForUpdate = obj.PhieuSua.FirstOrDefault(m => m.Id == t.Id);
                objForUpdate.NoiDung = t.NoiDung;
                objForUpdate.ProblemBy = t.ProblemBy;
                objForUpdate.Type = t.Type;
                //objForUpdate.Status = t.Status;
                objForUpdate.ProductId = t.ProductId;
                objForUpdate.ThoId = t.ThoId;
                objForUpdate.SoTien = t.Type == (byte)PhieuSuaType.BaoHanh ? 0 : t.SoTien;
            });
            context.PhieuSua.RemoveRange(listRemovePhieusua);
            #endregion

            base.Update(obj);
        }
    }
}