using GalaSoft.MvvmLight.Command;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Net;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using WpfAppParsePicture.Model;

namespace WpfAppParsePicture.ViewModel
{
    public class ApplicationViewModel : INotifyPropertyChanged
    {
        private readonly string apiKey = "34119621-cd62b9995da20c163b58de4f7";
        private RelayCommand searchCommand;
        private RelayCommand downloadAllCommand;
        private RelayCommand downloadOneCommand;
        private string myQuery;
        private PixaBayImage selectedImage;

        public ObservableCollection<Model.PixaBayImage> MyPicture { get; set; }

        public ApplicationViewModel()
        {
            
        }

        public RelayCommand SearchCommand
        {
            get 
            {
                if (searchCommand == null) searchCommand = new RelayCommand(SearchImage);
                return searchCommand; 
            }
        }

        public RelayCommand DownloadAllCommand
        {
            get
            {
                if (downloadAllCommand == null) downloadAllCommand = new RelayCommand(DownloadAll);
                return downloadAllCommand;
            }
            
        }

        public RelayCommand DownloadOneCommand
        {
            get
            {
                if (downloadOneCommand == null) downloadOneCommand = new RelayCommand(DownloadOne);
                return downloadOneCommand;
            }

        }

        public string MyQuery
        {
            get 
            {
                return myQuery;
            }
            set 
            { 
                myQuery = value;
                OnPropertyChanged("MyQuery");
            }
        }

        public Model.PixaBayImage SelectedImage
        {
            get { return selectedImage ; }
            set
            {
                selectedImage = value;
                OnPropertyChanged("SelectedImage");
            }
        }

        private void SearchImage()
        {
            using (WebClient client = new WebClient())
            {
                MyPicture = new ObservableCollection<PixaBayImage>();
                if (MyQuery == null) return;
                string query = MyQuery.Replace(" ", "+");
                string apiUrl = $"https://pixabay.com/api/?key={apiKey}&q={query}&image_type=all";
                
                try
                {
                    string json = client.DownloadString(apiUrl);
                    PixaBayResponse picture = Newtonsoft.Json.JsonConvert.DeserializeObject<PixaBayResponse>(json);
                    if (picture != null)
                    {
                        foreach (Model.PixaBayImage hit in picture.Hits)
                        {
                            MyPicture.Add(hit);
                        }
                    }
                    else
                    {
                        MessageBox.Show("Ошибка при десериализации JSON.");
                    }
                }
                catch (WebException ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        private void DownloadAll()
        {
            //if (MyPicture.Count == 0)
            //{
            //    SearchImage();
            //}
            int i = 1;
            foreach (var item in MyPicture)
            {
                using(WebClient client = new WebClient())
                {
                    try
                    {
                        client.DownloadFile(item.WebformatURL, $@"D:\\image\{item.Id}.{item.Type}");
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                        //throw;
                    }
                    
                }
            }
        }

        private void DownloadOne()
        {
            using (WebClient client = new WebClient())
            {
                client.DownloadFile(SelectedImage.ImageURL, $@"D:\\{SelectedImage.Id}.{SelectedImage.Type}");
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }
    }
}
