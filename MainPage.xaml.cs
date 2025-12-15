using Microsoft.Data.SqlClient;

namespace Lab10
   
{
    public partial class MainPage : ContentPage
    {
   

        public MainPage()
        {
            InitializeComponent();
            if (Preferences.ContainsKey("Name"))
            {
                Navigation.PushAsync(new Page1());
            }
        }

        private void Button_Clicked(object sender, EventArgs e) // login button
        {
            SqlConnection con = new SqlConnection("Data Source=SQL8006.site4now.net;Initial Catalog=db_ac1f36_sky1212;User Id=db_ac1f36_sky1212_admin;Password=q1w2e3r4T5Y6U7I8");
            String sql;
            sql = "SELECT * FROM usertable where username = '"+ txt1.Text + "' and userpass = '" + txt2.Text +"' ";
            SqlCommand comm = new SqlCommand(sql, con);
            con.Open();
            SqlDataReader reader = comm.ExecuteReader();
            if (reader.Read())
            {
                Preferences.Set("Name", txt1.Text);
                Preferences.Set("Pass", txt2.Text);
                Navigation.PushAsync(new Page1());
            }
            else
                DisplayAlert("Alert", "Wrong username or password", "OK");
            reader.Close();
            con.Close();

        



        }

    }

}
