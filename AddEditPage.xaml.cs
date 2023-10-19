﻿using System;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ThirdLaboratoryWork
{
    /// <summary>
    /// Логика взаимодействия для AddEditPage.xaml
    /// </summary>
    public partial class AddEditPage : Page
    {
        private Service _currentService = new Service();
        public AddEditPage(Service SelectedService)
        {
            InitializeComponent();
            if (SelectedService != null)
            {
                _currentService = SelectedService;
            }
            DataContext = _currentService;
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            StringBuilder errors = new StringBuilder();

            if (string.IsNullOrWhiteSpace(_currentService.Title))
            {
                errors.AppendLine("Укажите название услуги");
            }
            if (_currentService.Cost == 0)
            {
                errors.AppendLine("Укажите стоимость услуги");
            }
            if (string.IsNullOrWhiteSpace(_currentService.Discount.ToString())) _currentService.Discount = 0;

            if (_currentService.Discount < 0)
            {
                errors.AppendLine("Укажите скидку");
            }
            if (_currentService.Discount > 100)
            {
                errors.AppendLine("Невозможно указать такую скидку");
            }
            if (string.IsNullOrWhiteSpace(_currentService.DurationInSeconds))
            {
                errors.AppendLine("Укажите длительность услуги");
            }
            if (errors.Length > 0)
            {
                MessageBox.Show(errors.ToString());
                return;
            }
            if (_currentService.ID == 0)
            {
                GilmansginAutoServiceEntities.GetContext().Service.Add(_currentService);
            }

            try
            {
                GilmansginAutoServiceEntities.GetContext().SaveChanges();
                MessageBox.Show("информация сохранена");
                Manager.MainFrame.GoBack();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
        }
    }
}
