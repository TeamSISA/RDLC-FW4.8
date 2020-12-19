using Dominio;
using Microsoft.Reporting.WinForms;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RDLC_FW
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }


        private void getSalesReport(DateTime startDate, DateTime endDate)
        {
            SalesReport reportModel = new SalesReport();
            reportModel.createSalesOrderReport(startDate, endDate);
            SalesReportBindingSource.DataSource = reportModel;
            SalesListingBindingSource.DataSource = reportModel.salesListing;
            NetSalesByPeriodBindingSource.DataSource = reportModel.netSalesByPeriod;
            this.reportViewer2.ProcessingMode = ProcessingMode.Local;
            this.reportViewer2.LocalReport.DataSources.Clear();
            ReportDataSource rs1 = new ReportDataSource();
            rs1.Name = "salesReport";
            rs1.Value = SalesReportBindingSource;
            ReportDataSource rs2 = new ReportDataSource();
            rs2.Name = "salesListing";
            rs2.Value = SalesListingBindingSource;
            ReportDataSource rs3 = new ReportDataSource();
            rs3.Name = "netSalesByPeriod";
            rs3.Value = NetSalesByPeriodBindingSource;
            this.reportViewer2.LocalReport.DataSources.Add(rs1);
            this.reportViewer2.LocalReport.DataSources.Add(rs2);
            this.reportViewer2.LocalReport.DataSources.Add(rs3);
            reportViewer2.LocalReport.ReportEmbeddedResource = "RDLC_FW.Reports.SalesReport.rdlc";
            this.reportViewer2.RefreshReport();
        }

        private void btnToday_Click(object sender, EventArgs e)
        {
            var fromDate = DateTime.Today;
            var toDate = DateTime.Now;
            getSalesReport(fromDate, toDate);
        }

        private void btnLast7Days_Click(object sender, EventArgs e)
        {
            var fromDate = DateTime.Today.AddDays(-7);
            var toDate = DateTime.Now;
            getSalesReport(fromDate, toDate);
        }

        private void btnThisMonth_Click(object sender, EventArgs e)
        {
            var fromDate = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
            var toDate = DateTime.Now;
            getSalesReport(fromDate, toDate);
        }

        private void btnLast30Days_Click(object sender, EventArgs e)
        {
            var fromDate = DateTime.Today.AddDays(-30);
            var toDate = DateTime.Now;
            getSalesReport(fromDate, toDate);
        }

        private void btnThisYear_Click(object sender, EventArgs e)
        {
            var fromDate = new DateTime(DateTime.Now.Year, 1, 1);
            var toDate = DateTime.Now;
            getSalesReport(fromDate, toDate);
        }

        private void btnApplyCustomDate_Click(object sender, EventArgs e)
        {
            var fromDate = dateTimePickerFromDate.Value;
            var toDate = dateTimePickerToDate.Value;
            getSalesReport(fromDate, new DateTime(toDate.Year, toDate.Month, toDate.Day, 23, 59, 59));
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            
        }
    }
}
