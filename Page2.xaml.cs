using Microsoft.Data.SqlClient;

namespace Lab10;

public partial class Page2 : ContentPage
{
    static List<Book> BuyBooks = new List<Book>();
    public Page2(Book bk)

    {
        InitializeComponent();
        bool flage = false;
        foreach (var book in BuyBooks.ToList())
        {
            if (book.Title == bk.Title)
                flage = true;
        }
        if (flage == false)
            BuyBooks.Add(bk);
        myListView.ItemsSource = BuyBooks;
    }
    private void Button_Clicked(object sender, EventArgs e)
    {
        Navigation.PushAsync(new Page1());
    }
    private void Button_Clicked_1(object sender, EventArgs e)
    {
        SqlConnection con = new SqlConnection("Data Source=SQL8006.site4now.net;Initial Catalog=db_ac1f36_sky1212;User Id=db_ac1f36_sky1212_admin;Password=q1w2e3r4T5Y6U7I8");
        string sql = "";
        string custname = Preferences.Get("Name", " "); // login Local Storage  - Preferences
        sql = "insert into bookorder (custname,orderdate,total) values ( '" + custname + "' , '" + null + "', '0' )";
        con.Open();
        SqlCommand comm = new SqlCommand(sql, con);
        comm.ExecuteNonQuery();
        con.Close();

        sql = "select * from bookorder  where custname= '" + custname + "'  order by id desc ";
        int ordid = 0;
        comm = new SqlCommand(sql, con);
        con.Open();
        SqlDataReader reader = comm.ExecuteReader();
        if (reader.Read())
        {
            ordid = (int)reader["id"];
        }
        else
            reader.Close();
        con.Close();
        int tot = 0;
        foreach (var book in BuyBooks.ToList())
        {
            if (book.quant > 0)
            {
                sql = "insert into  orderline (orderid,itemname,itemquant,itemprice) values ( '" + ordid + "'  ,'" + book.Title + "' ,'" + book.quant + "','" + book.Price + "') ";
                comm = new SqlCommand(sql, con);
                con.Open();
                comm.ExecuteNonQuery();
                con.Close();
                sql = "UPDATE book  SET bookquantity  = bookquantity - '" + book.quant + "'  where  title ='" + book.Title + "' ";
                comm = new SqlCommand(sql, con);
                con.Open();
                comm.ExecuteNonQuery();
                con.Close();
                tot = tot + (book.quant * book.Price);
            }
        }
        sql = "UPDATE bookorder  SET total  = '" + tot + "'   where  Id ='" + ordid + "' ";
        comm = new SqlCommand(sql, con);
        con.Open();
        comm.ExecuteNonQuery();
        con.Close();
        DisplayAlert("Alert", "Thank You for your purchase you need to pay" + tot, "OK");
    }

    private void ImageButton_Clicked(object sender, EventArgs e)
    {
        if (Preferences.ContainsKey("Name"))

        {

            Preferences.Remove("Pass");
            Preferences.Remove("Name");


            Navigation.PushAsync(new MainPage()); //login page


        }

    }
}

