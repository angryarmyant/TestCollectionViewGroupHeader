using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TestCollectionViewGroupHeader
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CollectionViewGroupHeader : ContentView
    {
        public CollectionViewGroupHeader()
        {
            InitializeComponent();
        }

        public static readonly BindableProperty HeaderTextProperty = BindableProperty.Create(nameof(HeaderText), typeof(string), typeof(CollectionViewGroupHeader), propertyChanged: OnHeaderTextChanged);
        private static void OnHeaderTextChanged(BindableObject bindable, object oldValue, object newValue)
        {
            ((CollectionViewGroupHeader)bindable).SetHeaderText((string)newValue);
        }
        private void SetHeaderText(string text)
        {
            this.Lbl.Text = text;
        }
        public string HeaderText
        {
            get => (string)GetValue(HeaderTextProperty);
            set => SetValue(HeaderTextProperty, value);
        }
    }
}