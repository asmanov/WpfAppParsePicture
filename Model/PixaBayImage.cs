using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace WpfAppParsePicture.Model
{
    public class PixaBayImage : INotifyPropertyChanged
    {
        private int id;
        private string webformatURL;
        private string imageURL;
        private string type;

        public int Id 
        { 
            get { return id; }
            set 
            { 
                id = value;
                OnPropertyChanged("Id");
            } 
        }
        public string WebformatURL 
        { 
            get { return webformatURL; }
            set
            {
                webformatURL = value;
                OnPropertyChanged("WebformatURL");
            } 
        }
        public string ImageURL 
        {
            get { return imageURL; } 
            set
            {
                imageURL = value;
                OnPropertyChanged("ImageURL");
            }
        }
        public string Type 
        { 
            get { return type; }
            set
            {
                type = value;
                OnPropertyChanged("Type");
            } 
        }

        public PixaBayImage()
        {

        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }
    }
}
