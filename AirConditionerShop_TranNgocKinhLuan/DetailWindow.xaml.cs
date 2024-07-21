using AirConditionerShop.BLL.Services;
using AirConditionerShop.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace AirConditionerShop_TranNgocKinhLuan
{
    /// <summary>
    /// Interaction logic for DetailWindow.xaml
    /// </summary>
    public partial class DetailWindow : Window
    {
        private AirConditionerService _airService = new();
        private SupplierService _supplierService = new();
        public AirConditioner SelectedAirConditioner
        { get; set; } = null;
        public DetailWindow()
        {
            InitializeComponent();
        }
        
        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(AirConsIdTextBox.Text) || 
                string.IsNullOrWhiteSpace(AirConsNameTextBox.Text) ||
                string.IsNullOrWhiteSpace(WarrantyTextBox.Text) ||
                string.IsNullOrWhiteSpace(SoundPressureLevelTextBox.Text) ||
                string.IsNullOrWhiteSpace(PriceTextBox.Text) ||
                string.IsNullOrWhiteSpace(FeatureFunctionTextBox.Text) ||
                string.IsNullOrWhiteSpace(QuantityTextBox.Text) ||
                string.IsNullOrWhiteSpace(PriceTextBox.Text) ||
                SupplierNameComboBox.SelectedValue == null)  
                {
                MessageBox.Show("All fields are required!", "Warning", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            //int.TryParse(AirConsIdTextBox.Text, out int id);
            //if (_airService.GetById(id) != null)
            //{
            //    MessageBox.Show("This ID is already existed!", "Warning", MessageBoxButton.OK, MessageBoxImage.Error);
            //    return;
            //}
            if (!int.TryParse(QuantityTextBox.Text, out int quantity) ||
              !double.TryParse(PriceTextBox.Text, out double dollarPrice) ||
              quantity < 0 || quantity >= 4000000 ||
              dollarPrice < 0 || dollarPrice >= 4000000)
            {
                MessageBox.Show("Quantity and Dollar Price must be between 0 and 4,000,000.", "Validation Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            string airConditionerName = AirConsNameTextBox.Text;
            if (airConditionerName.Length < 5 || airConditionerName.Length > 90 || !airConditionerName.Split().All(word => char.IsUpper(word[0]) || char.IsDigit(word[0])))
            {
                MessageBox.Show("AirConditionerName must be 5-90 characters long, and each word must begin with a capital letter or digit (1-9).", "Validation Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            AirConditioner airConditioner = new();
            airConditioner.AirConditionerId = int.Parse(AirConsIdTextBox.Text);
            airConditioner.AirConditionerName = AirConsNameTextBox.Text;
            airConditioner.Warranty = WarrantyTextBox.Text;
            airConditioner.SoundPressureLevel = SoundPressureLevelTextBox.Text;
            airConditioner.FeatureFunction = FeatureFunctionTextBox.Text;
            airConditioner.Quantity = int.Parse(QuantityTextBox.Text);
            airConditioner.DollarPrice = double.Parse(PriceTextBox.Text);
            airConditioner.SupplierId = SupplierNameComboBox.SelectedValue.ToString();
            if (SelectedAirConditioner != null)
            {
                _airService.Update(airConditioner);
                MessageBox.Show("Update successfully!", "Updated!", MessageBoxButton.OK, MessageBoxImage.Information);
                this.Close();
                return;
             }
            MessageBox.Show("Add successfully!", "Added!", MessageBoxButton.OK, MessageBoxImage.Information);
            _airService.Add(airConditioner);
            this.Close();
            return;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            FillComboBox();
            if (SelectedAirConditioner != null)
            {
                AirConsIdTextBox.Text = SelectedAirConditioner.AirConditionerId.ToString();
                AirConsNameTextBox.Text = SelectedAirConditioner.AirConditionerName;
                WarrantyTextBox.Text = SelectedAirConditioner.Warranty;
                SoundPressureLevelTextBox.Text = SelectedAirConditioner.SoundPressureLevel;
                FeatureFunctionTextBox.Text = SelectedAirConditioner.FeatureFunction;
                QuantityTextBox.Text = SelectedAirConditioner.Quantity.ToString();
                PriceTextBox.Text = SelectedAirConditioner.DollarPrice.ToString();
                SupplierNameComboBox.SelectedValue = SelectedAirConditioner.SupplierId;
            }
        }

        private void FillComboBox()
        {
            SupplierNameComboBox.ItemsSource = _supplierService.GetAll();
            SupplierNameComboBox.DisplayMemberPath = "SupplierName";
            SupplierNameComboBox.SelectedValuePath = "SupplierId";
        }
    }
}
