using AirConditionerShop.BLL.Services;
using AirConditionerShop.DAL.Entities;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace AirConditionerShop_TranNgocKinhLuan
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public StaffMember CurrentUser { get; set; } = null;
        private AirConditionerService _service = new();

        public MainWindow(StaffMember staff)
        {
            InitializeComponent();
            CurrentUser = staff;
        }

        private bool IsAdmin()
        {
            return CurrentUser?.Role == 1;
        }

        private void AirConListDataGrid_Loaded(object sender, RoutedEventArgs e)
        {
            LoadList();
        }

        private void QuitButton_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void LoadList()
        {
            AirConListDataGrid.ItemsSource = null;
            AirConListDataGrid.ItemsSource = _service.GetAll();
        }

        private void SearchButton_Click(object sender, RoutedEventArgs e)
        {
            // chửi nếu gõ cà chớn quantity and do nothing
            int? quantity = null;
            int tmpQuantity; // hứng giá trị convert thành công, hoặc ngoctrinh mang0
            bool quantStatus = int.TryParse(QuantityTextBox.Text, out tmpQuantity);
            // convert ko đc vì 1 trong 2 lí do: không nhập gì cả, hoặc nhập text ko phải số
            if (!quantStatus && !QuantityTextBox.Text.IsNullOrEmpty()) // nếu gõ tử tế số lượng thì lấy số lượng để dùng, ngược lại của true là gõ cà chớn chửi
            {
               MessageBox.Show("Quantity must be a number!", "Invalid!", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            // else có khả năng là conver thất bại luôn do bỏ trống, hoặc thành công
            else if (quantStatus)
            {
                quantity = tmpQuantity;
            }
            var result = _service.SearchOr(FeatureFuntionTextBox.Text, quantity);
            AirConListDataGrid.ItemsSource = null;
            AirConListDataGrid.ItemsSource = result;

        }

        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            if (IsAdmin())
            {
                var selected = (dynamic)AirConListDataGrid.SelectedItem;
                if (selected == null)
                {
                    MessageBox.Show("Please select an air-conditioner to delete!", "Warning!", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }
                MessageBoxResult result = MessageBox.Show("Do you want to delete this air-conditioner", "Warning!", MessageBoxButton.YesNo, MessageBoxImage.Question);
                if (result == MessageBoxResult.No) return;
                _service.Delete(selected);
                LoadList();
                MessageBox.Show("Delte air-conditioner successfully!", "Deleted!", MessageBoxButton.OK, MessageBoxImage.Information);

            }
            else
            {
                MessageBox.Show("You have no permission to access this function!", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
        }

        private void CreateButton_Click(object sender, RoutedEventArgs e)
        {
            if (IsAdmin())
            {
                DetailWindow d = new DetailWindow();
                d.ShowDialog();
                LoadList();
            }
            else
            {
                MessageBox.Show("You have no permission to access this function!", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
        }

        private void UpdateButton_Click(object sender, RoutedEventArgs e)
        {
            if (IsAdmin())
            {
                var selected = (dynamic)AirConListDataGrid.SelectedItem;
                if (selected == null)
                {
                    MessageBox.Show("Please select an air-conditioner to update!", "Warning!", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }
                DetailWindow d = new DetailWindow();
                d.SelectedAirConditioner = selected;
                d.ShowDialog();
                LoadList();
            }
        }
    }
}
