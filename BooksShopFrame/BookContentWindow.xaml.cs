using BLL.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace BooksShopFrame
{
    /// <summary>
    /// Interaction logic for BookContentWindow.xaml
    /// </summary>
    public partial class BookContentWindow : Window
    {
        private Book book;
        public BookContentWindow(Book selectedBook)
        {
            InitializeComponent();
            book = selectedBook;
            ReedBooks();
        }

        private void ReedBooks()
        {
            if (book.Name.Contains("Граф Монте-Крісто")) 
                readerTB.Text = File.ReadAllText("Graf.txt");
            if (book.Name.Contains("Береги ЛК"))
                readerTB.Text = File.ReadAllText("Beregi.txt");
            if (book.Name.Contains("Пророк"))
                readerTB.Text = File.ReadAllText("Proroc.txt");
        }
    }
}
