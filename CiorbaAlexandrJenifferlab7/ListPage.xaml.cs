namespace CiorbaAlexandrJenifferlab7;
using CiorbaAlexandrJenifferlab7.Models;
    public partial class ListPage : ContentPage
    {
        public ListPage()
        {
            InitializeComponent();
        }

        async void OnSaveButtonClicked(object sender, EventArgs e)
        {
            var slist = (ShopList)BindingContext;
            slist.Date = DateTime.UtcNow;
            await App.Database.SaveShopListAsync(slist);
            await Navigation.PopAsync();
        }

        async void OnDeleteButtonClicked(object sender, EventArgs e)
        {
            //var slist = (ShopList)BindingContext;
            //await App.Database.DeleteShopListAsync(slist);
            //await Navigation.PopAsync();

            // 1. Verifică dacă un produs a fost selectat în ListView
            var selectedProduct = listView.SelectedItem as Product;

            // 2. Preia ID-ul listei curente (pe care o editezi) din BindingContext
            var currentShopList = BindingContext as ShopList;

            if (selectedProduct == null || currentShopList == null)
            {
                // Nu permite ștergerea dacă nu este selectat nimic
                await DisplayAlert("Eroare", "Selectați un produs din listă pentru ștergere.", "OK");
                return;
            }

            // 3. Execută ștergerea legăturii (ListProduct) din baza de date
            await App.Database.DeleteListProductAsync(currentShopList.ID, selectedProduct.ID);

            // 4. Reîmprospătează lista de produse afișate
            listView.ItemsSource = await App.Database.GetListProductsAsync(currentShopList.ID);

            // (Opțional) Deselectează elementul pentru a preveni ștergeri accidentale
            listView.SelectedItem = null;
        }

        async void OnChooseButtonClicked(object sender, EventArgs e)
        {

            await Navigation.PushAsync(new ProductPage((ShopList)
                this.BindingContext)
            {
                BindingContext = new Product()
            });

        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();

            var shopl = (ShopList)BindingContext;

            listView.ItemsSource = await App.Database.GetListProductsAsync(shopl.ID);
        }
    }
