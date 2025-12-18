namespace MAUI_16_2;

public partial class NewPage2 : ContentPage
{
    public NewPage2()
    {
        InitializeComponent();
    }

    private void TipSlider_ValueChanged(object sender, ValueChangedEventArgs e)
    {
        int percent = (int)Math.Round(e.NewValue);

        if (TipSlider.Value != percent)
            TipSlider.Value = percent;

        TipPercentLabel.Text = $"{percent} %";
    }

    private void PeopleStepper_ValueChanged(object sender, ValueChangedEventArgs e)
    {
        PeopleLabel.Text = e.NewValue.ToString();
    }

    private void CalculateButton_Clicked(object sender, EventArgs e)
    {
        if (!double.TryParse(BillAmountEntry.Text, out double billAmount))
            return;

        double tipPercent = TipSlider.Value / 100;
        int peopleCount = (int)PeopleStepper.Value;

        double tipAmount = billAmount * tipPercent;
        double totalAmount = billAmount + tipAmount;
        double perPerson = totalAmount / peopleCount;

        TipAmountLabel.Text = $"Napiwek: {tipAmount:F2} z³";
        TotalAmountLabel.Text = $"Suma: {totalAmount:F2} z³";
        PerPersonLabel.Text = $"Na osobê: {perPerson:F2} z³";
    }

    private void ClearButton_Clicked(object sender, EventArgs e)
    {
        BillAmountEntry.Text = string.Empty;
        TipSlider.Value = 15;
        PeopleStepper.Value = 1;

        TipAmountLabel.Text = "Napiwek: 0.00 z³";
        TotalAmountLabel.Text = "Suma: 0.00 z³";
        PerPersonLabel.Text = "Na osobê: 0.00 z³";
    }
}
