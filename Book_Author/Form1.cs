using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Book_Author
{
    public partial class Form1 : Form
    {
        DAO db = new DAO();

        public Form1()
        {
            InitializeComponent();
        }

        private void cbBook_SelectedIndexChanged(object sender, EventArgs e)
        {
            List<Book> books = db.GetBooks();
            Book book = (Book) cbBook.SelectedItem;

            cbYear.SelectedIndex = book.Year - 2000;
            lbAuthor.DataSource = book.authors;
            lbAuthor.DisplayMember = "Name";
            lbAuthor.ValueMember = "ID";
        }

        public void reload()
        {
            List<Book> books = db.GetBooks();
            cbBook.DataSource = books;
            cbBook.DisplayMember = "title";
            cbBook.ValueMember = "ID";
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            reload();
        }

        private void btnRemove_Click(object sender, EventArgs e)
        {
            Author author = (Author)lbAuthor.SelectedItem;
            Book book = (Book)cbBook.SelectedItem;
            db.deleteAuthor(book.ID, author.ID);
            reload();
        }
    }
}
