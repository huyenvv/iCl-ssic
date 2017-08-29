using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using iClassic.Models;
using iClassic.Helper;

namespace iClassic.Services.Implementation
{
    public class ReportServices
    {
        private readonly PhieuSanXuatRepository _phieuSanXuatRepository;
        private readonly PhieuSuaRepository _phieuSuaRepository;
        private readonly PhieuChiRepository _phieuChiRepository;
        private readonly CustomerRepository _customerRepository;

        public ReportServices(iClassicEntities entities)
        {
            _phieuSanXuatRepository = new PhieuSanXuatRepository(entities);
            _phieuSuaRepository = new PhieuSuaRepository(entities);
            _phieuChiRepository = new PhieuChiRepository(entities);
            _customerRepository = new CustomerRepository(entities);
        }

        public ReportGraphResult[] Graph(StatisticSearch model)
        {
            ReportTypes type = model.Type.HasValue ? model.Type.Value : ReportTypes.SevenDaysRecent;

            #region Build Date & Column
            var now = DateTime.Now;
            DateTime startDate = now.Date.AddDays(-6);
            int column = 7;
            switch (type)
            {
                case ReportTypes.ThisMonth:
                    startDate = new DateTime(now.Year, now.Month, 1);
                    column = DateTime.DaysInMonth(startDate.Year, startDate.Month);
                    break;
                case ReportTypes.LastMonth:
                    startDate = new DateTime(now.Year, now.Month, 1).AddMonths(-1);
                    column = DateTime.DaysInMonth(startDate.Year, startDate.Month);
                    break;
                case ReportTypes.SixMonthRecent:
                    startDate = new DateTime(now.Year, now.Month, 1).AddMonths(-5);
                    column = 6;
                    break;
                case ReportTypes.AllTime:
                    startDate = new DateTime(now.Year, now.Month, 1).AddYears(-4);
                    column = 5;
                    break;
            }
            #endregion

            #region Build Data
            var data = new ReportGraphResult[column];
            var tongThuSanXuat = _phieuSanXuatRepository.GetByDateRange(model.BranchId, startDate, now);
            var tongThuSuachua = _phieuSuaRepository.GetByDateRange(model.BranchId, startDate, now);
            var tongChi = _phieuChiRepository.GetByDateRange(model.BranchId, startDate, now);

            for (int i = 1; i <= column; i++)
            {
                var item = new ReportGraphResult();

                #region Build EndDate
                DateTime endate;
                switch (type)
                {
                    case ReportTypes.SixMonthRecent:
                        endate = startDate.AddMonths(1).AddTicks(-1);
                        break;
                    case ReportTypes.AllTime:
                        endate = startDate.AddYears(1).AddTicks(-1);
                        break;
                    default:
                        endate = startDate.AddDays(1).AddTicks(-1);
                        break;
                }
                #endregion

                var sanxuat = tongThuSanXuat.Where(m => startDate <= m.NgayLay && m.NgayLay <= endate).Sum(m => (float?)(m.DonGia + m.LoaiVai.SoTien) * m.SoLuong);
                var suachua = tongThuSuachua.Where(m => startDate <= m.NgayTra && m.NgayTra <= endate).Sum(m => (float?)(m.SoTien));
                var chi = tongChi.Where(m => startDate <= m.Created && m.Created <= endate).Sum(m => (float?)(m.SoTien));
                item.Thu = (sanxuat ?? 0) + (suachua ?? 0);
                item.Chi = chi ?? 0;
                item.LoiNhuan = item.Thu - item.Chi;
                #region item.Time
                switch (type)
                {
                    case ReportTypes.SixMonthRecent:
                        item.Time = startDate.ToString("MM/yyyy");
                        break;
                    case ReportTypes.AllTime:
                        item.Time = startDate.ToString("yyyy");
                        break;
                    default:
                        item.Time = startDate.ToString("dd/MM");
                        break;
                }
                #endregion

                #region ReBuild StartDate
                switch (type)
                {
                    case ReportTypes.SixMonthRecent:
                        startDate = startDate.AddMonths(1);
                        break;
                    case ReportTypes.AllTime:
                        startDate = startDate.AddYears(1);
                        break;
                    default:
                        startDate = startDate.AddDays(1);
                        break;
                }
                #endregion

                data[i - 1] = item;
            }
            #endregion

            return data;
        }

        public ReportProfit GetProfit(StatisticSearch model)
        {
            var tongThuSanXuat = _phieuSanXuatRepository.GetByDateRange(model.BranchId, model.StartDate, model.EndDate).Sum(m => (float?)(m.DonGia + m.LoaiVai.SoTien) * m.SoLuong);
            var tongThuSuachua = _phieuSuaRepository.GetByDateRange(model.BranchId, model.StartDate, model.EndDate).Sum(m => (float?)(m.SoTien));
            var tongChi = _phieuChiRepository.GetByDateRange(model.BranchId, model.StartDate, model.EndDate).Sum(m => (float?)(m.SoTien));

            var profit = new ReportProfit();
            profit.Thu = (tongThuSanXuat ?? 0) + (tongThuSuachua ?? 0);
            profit.Chi = tongChi ?? 0;
            profit.LoiNhuan = profit.Thu - profit.Chi;
            return profit;
        }

        public IQueryable<ReportCustomVip> GetCustomerVip(StatisticSearch model)
        {
            var allCustomers = _customerRepository.GetByBranchId(model.BranchId)
                .Select(m => new ReportCustomVip
                {
                    Customer = m,
                    SoLanMay = m.PhieuSanXuat.Count,
                    SoLanSua = m.PhieuSua.Count
                });
            return allCustomers;
        }
    }
}