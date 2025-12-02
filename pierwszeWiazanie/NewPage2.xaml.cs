using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;

namespace pierwszeWiazanie
{
    public partial class NewPage2 : ContentPage
    {
        public ShoppingListViewModel ViewModel { get; set; }

        public NewPage2()
        {
            InitializeComponent();
            ViewModel = new ShoppingListViewModel();
            BindingContext = ViewModel;
        }

        private void AddButton_Clicked(object sender, EventArgs e)
        {
            ViewModel.AddItem();
        }

        private void RemoveButton_Clicked(object sender, EventArgs e)
        {
            var button = sender as Button;
            var item = button.BindingContext as ShoppingItem;
            ViewModel.RemoveItem(item);
        }
    }

    // -------------------------------
    // SHOPPING ITEM
    // -------------------------------

    public class ShoppingItem : INotifyPropertyChanged
    {
        private string name;
        public string Name
        {
            get => name;
            set { name = value; OnPropertyChanged(nameof(Name)); OnPropertyChanged(nameof(TotalPrice)); }
        }

        private decimal price;
        public decimal Price
        {
            get => price;
            set { price = value; OnPropertyChanged(nameof(Price)); OnPropertyChanged(nameof(TotalPrice)); }
        }

        private int quantity;
        public int Quantity
        {
            get => quantity;
            set { quantity = value; OnPropertyChanged(nameof(Quantity)); OnPropertyChanged(nameof(TotalPrice)); }
        }

        public decimal TotalPrice => Price * Quantity;

        public event PropertyChangedEventHandler PropertyChanged;
        void OnPropertyChanged(string name) => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
    }

    // -------------------------------
    // VIEWMODEL LISTY
    // -------------------------------

    public class ShoppingListViewModel : INotifyPropertyChanged
    {
        public ObservableCollection<ShoppingItem> Items { get; set; }
            = new ObservableCollection<ShoppingItem>();

        public string NewName { get; set; }
        public decimal NewPrice { get; set; }
        public int NewQuantity { get; set; } = 1;

        public ShoppingListViewModel()
        {
            Items.CollectionChanged += (_, __) => OnPropertyChanged(nameof(TotalSum));
        }

        public decimal TotalSum => Items.Sum(i => i.TotalPrice);

        public void AddItem()
        {
            if (string.IsNullOrWhiteSpace(NewName) || NewPrice <= 0 || NewQuantity <= 0)
                return;

            Items.Add(new ShoppingItem
            {
                Name = NewName,
                Price = NewPrice,
                Quantity = NewQuantity
            });

            NewName = "";
            NewPrice = 0;
            NewQuantity = 1;

            OnPropertyChanged(nameof(TotalSum));
            OnPropertyChanged(nameof(NewName));
            OnPropertyChanged(nameof(NewPrice));
            OnPropertyChanged(nameof(NewQuantity));
        }

        public void RemoveItem(ShoppingItem item)
        {
            Items.Remove(item);
            OnPropertyChanged(nameof(TotalSum));
        }

        public event PropertyChangedEventHandler PropertyChanged;
        void OnPropertyChanged(string name) => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
    }
}