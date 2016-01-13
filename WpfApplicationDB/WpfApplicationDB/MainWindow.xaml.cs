using DataLayer;
using Models;
using Remotion.Linq.Collections;
using System.ComponentModel;
using System.Diagnostics;
using System.Windows;

namespace WpfApplicationDB
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            LoadStores();
            InitializeComponent();
            this.DataContext = this;
        }

        private void LoadStores()
        {
            NHDataContext.PopulateDB();
            using (var session = NHDataContext.CreateSessionFactory().OpenSession())
            {
                var stores = session.GetStores();
                foreach (var store in stores)
                {
                    Stores.Add(store);
                    Debug.WriteLine(store.Name);
                }
            }
           Debug.Assert(!string.IsNullOrEmpty(Stores[0].Products[0].Name));
        }

        public ObservableCollection<Store> Stores
        {
            get { return (ObservableCollection<Store>)GetValue(StoresProperty); }
            set { SetValue(StoresProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Stores.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty StoresProperty =
            DependencyProperty.Register("Stores", typeof(ObservableCollection<Store>), typeof(MainWindow), new PropertyMetadata(new ObservableCollection<Store>()));


    }
}
