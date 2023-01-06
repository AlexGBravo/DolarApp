namespace ActualDolar;

public partial class MainPage : ContentPage
{

    // Valor en COP de dolares
    double Value = 0;


    /// <summary>
    /// Inicio de la pagina
    /// </summary>
    public MainPage()
    {
        InitializeComponent();
        LoadData();
    }



    /// <summary>
    /// Visualiza los datos
    /// </summary>
    private void LoadData()
    {

        // Respuesta
        lbRes.Text = "Espera...";

        // Obtiene los datos
        var data = DolarLIB.Data.FethData();


        //====== Conectado ======//
        if (!data.Connected)
        {
            lbRes.Text = "No hay conexion";
            lbRes1.Text = "";
            lbBajando.Text = "▼ | ▲";
            lbRes.TextColor = Color.FromRgb(255, 79, 79);
            lbBajando.TextColor = Color.FromRgb(255, 158, 42);
            return;
        }

        // Bajo
        if (data.IsDown)
        {
            lbBajando.Text = "▼ Bajo";
            lbBajando.TextColor = Color.FromRgb(255, 79, 79);
        }

        // Subio
        else
        {
            lbBajando.Text = "▲ Subio";
            lbBajando.TextColor = Color.FromRgb(80, 175, 0);
        }


        // Muestra los datos correctos
        lbRes1.Text = "El dolar hoy esta a ";
        lbRes.Text = $" {data.Value}";
        lbRes.TextColor = Color.FromRgb(80, 175, 0);
        Value = double.Parse(data.Value);

    }


    /// <summary>
    /// Boton de recargar
    /// </summary>
    private void OnCounterClicked(object sender, EventArgs e)
    {
        LoadData();
    }




    // Evitan una saturacion del programa
    bool CopW = true; bool UsdW = true;


    // Cuando cambia el texto del campo txtCOP
    private void txtCOP_TextChanged(object sender, TextChangedEventArgs e)
    {
        if (txtCOP.Text == null || txtCOP.Text.Length == 0 || !CopW)
            return;

        // Desactiva el otro input
        UsdW = false;

        // Convercion de tipo
        double result = 0;
        {
            bool cv = double.TryParse(txtCOP.Text, out double n1);
            if (cv)
                result = n1 / Value;


        }

        txtUSD.Text = result.ToString();
        UsdW = true;
    }


    // Cuando cambia el texto del campo txtUSD
    private void txtUSD_TextChanged(object sender, TextChangedEventArgs e)
    {
        if (txtUSD.Text == null || txtUSD.Text.Length == 0 || !UsdW)
            return;

        CopW = false;
        double result = 0;
        {
            bool cv = double.TryParse(txtUSD.Text, out double n1);
            if (cv)
                result = n1 * Value;
        }
        txtCOP.Text = result.ToString();
        CopW = true;

    }

}

