using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace TestCollectionViewGroupHeader
{
    // Learn more about making custom code visible in the Xamarin.Forms previewer
    // by visiting https://aka.ms/xamarinforms-previewer
    [DesignTimeVisible(false)]
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();

            this.BindingContext = new ReportsViewModel(Reports);
        }

        protected override void OnSizeAllocated(double width, double height)
        {
            base.OnSizeAllocated(width, height);

            var col = Math.Floor(width / 75);

            var gap = width - col * 75;
            var hs = gap / (col + 1);        //每列之间，共col-1。左右两边+2，以 this.Padding 来体现

            //top, bottom 取原来的设置，可能是全局的样式设置！
            var top = this.Padding.Top;
            var bottom = this.Padding.Bottom;
            this.Padding = new Thickness(hs, top, hs, bottom);

            Cll.ItemsLayout = new GridItemsLayout((int)col, ItemsLayoutOrientation.Vertical)
            {
                VerticalItemSpacing = 0,        //由 Padding.Top 来起作用
                HorizontalItemSpacing = hs
            };
        }

        internal readonly static ImageSource DefaultIcon = ImageSource.FromResource("TestCollectionViewGroupHeader.Reports.gif");

        private readonly IEnumerable<Report> Reports = new Report[]
        {
            new Report()
            {
                Name= "用房统计",
                Category= "协议公司",
                IconSource = DefaultIcon
            },
            new Report()
            {
                Name = "用房明细",
                Category="协议公司",
                IconSource = DefaultIcon
            },
            new Report()
            {
                Name= "冲账",
                Category= "业务日志",
                IconSource = DefaultIcon
            },
            new Report()
            {
                Name = "收款",
                Category="业务日志",
                IconSource = DefaultIcon
            },
        };
    }

    public class Report
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Category { get; set; }
        public string Icon { get; set; }

        public ImageSource IconSource { get; set; }

        public override string ToString()
        {
            return this.Name;
        }
    }

    public class ReportCategory : List<Report>
    {
        public ReportCategory(string category, IEnumerable<Report> rs) : base(rs)
        {
            this.Category = category;
        }
        public string Category { get; private set; }
        public override string ToString()
        {
            return this.Category;
        }
    }

    public class ReportsViewModel
    {
        public ReportsViewModel(IEnumerable<Report> reports)
        {
            this.Reports = reports.GroupBy(r => r.Category).Select(kv => new ReportCategory(kv.Key, kv)).ToList();
        }

        public List<ReportCategory> Reports { get; private set; }
    }
}
