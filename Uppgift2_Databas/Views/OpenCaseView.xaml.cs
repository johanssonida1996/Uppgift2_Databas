using SharedLibrary;
using SharedLibrary.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;


// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace Uppgift2_Databas.Views
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class OpenCaseView : Page
    {
        public CaseListViewModel ViewModel { get; set; }        

        public OpenCaseView()       
        {

            var task = Task.Run(async () => await DataAccess.ReadJson());

            string statusRegistered = task.Result.StatusRegistered;
            string statusActive = task.Result.StatusActive;
            string statusClosed = task.Result.StatusClosed;            

            this.InitializeComponent();           
            ViewModel = new CaseListViewModel(statusRegistered, statusActive);
          
        }

        private async void tbUpdate_Click(object sender, RoutedEventArgs e)
        {
            Object selectedItem = cbStatus.SelectedItem;
            string status = Convert.ToString(selectedItem);
           
            await DataAccess.UpdateAsync(Convert.ToInt32(tbCaseNumber.Text), status);
            tbCaseNumber.Text = String.Empty;

            Frame.Navigate(Frame.CurrentSourcePageType);

        }      

     }
        
  }

