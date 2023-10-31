using BLL.Interfases;
using BLL.Models;
using BLL.Servises;
using System;
using System.Collections.Generic;
using System.ComponentModel;
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
using System.Windows.Media.Media3D;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace BooksShopFrame
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private BookServise _bookServise;
        private UserServise _userServise;
        private User currentUser;
        public MainWindow()
        {
            InitializeComponent();
            _bookServise = new BookServise();
            _userServise = new UserServise();
        }

        private void Interaction(User user)
        {
            productList.ItemsSource = _bookServise.GetAll();
            byProduct.ItemsSource = user.UserBooks;
            enterBT.Visibility = Visibility.Hidden;
            registrationBT.Visibility = Visibility.Hidden;
            playRadioBT.Visibility = Visibility.Visible;
            nameSerchBT.Visibility = Visibility.Visible;
            autorSerchBT.Visibility = Visibility.Visible;
            byBookBT.Visibility = Visibility.Visible;
            autorsSerchBT.Visibility = Visibility.Visible;
            newsSerchBT.Visibility = Visibility.Visible;
            topBooksSerchBT.Visibility = Visibility.Visible;
            genreSerchBT.Visibility = Visibility.Visible;
            readBookBT.Visibility = Visibility.Visible;

        }

        private void AdminInteraction(User user)
        {
            productList.ItemsSource = _bookServise.GetAll();
            byProduct.ItemsSource = user.UserBooks;
            enterBT.Visibility = Visibility.Hidden;
            registrationBT.Visibility = Visibility.Hidden;
            addBookBT.Visibility = Visibility.Visible;
            deleteBookBT.Visibility = Visibility.Visible;
            updateBookBT.Visibility = Visibility.Visible;
            playRadioBT.Visibility = Visibility.Visible;
            genreSerchBT.Visibility = Visibility.Visible;
            readBookBT.Visibility = Visibility.Visible;
        }

        private void playRadioBT_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            music.Source = new Uri("http://radio.ukr.radio:8000/ur1-te-mp3-m");
            music.Play();
        }

        private void registrationBT_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            registrationPop.IsOpen = true;
        }

        private void Border_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            registrationPop.IsOpen = false;
        }

        private void enterBT_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            loginPop.IsOpen = true;
        }

        private void Border_MouseLeftButtonUp_1(object sender, MouseButtonEventArgs e)
        {
            string login = regNameTB.Text;
            string pass = regPassTB.Text;

            User newUser = new User()
            {
                Login = login,
                Passworld = pass,
                UserBooks = new List<Book>()
            };

            if (_userServise.IsAdmin(login))
            {
                _userServise.Add(newUser);
                registrationPop.IsOpen = false;
                MessageBox.Show($"Вітаю {login} пухнастий адміністраторе!");
                AdminInteraction(newUser);
            }

            try
            {
                _userServise.Add(newUser);
                registrationPop.IsOpen = false;
                MessageBox.Show($"Вітаєм {login} у нашому магазині!");
                Interaction(newUser);
            }
            catch (InvalidOperationException ex)
            {
                registrationPop.IsOpen = false;
                MessageBox.Show(ex.Message, "Помилка реєстрації!");
            }
        }

        private void Border_MouseLeftButtonUp_2(object sender, MouseButtonEventArgs e)
        {
            loginPop.IsOpen = false;
        }

        private void Border_MouseLeftButtonUp_3(object sender, MouseButtonEventArgs e)
        {
            string login = logNameTB.Text;
            string password = logPassTB.Text;
            if (_userServise.TryLogin(login, password, out User loggedInUser))
            {
                loginPop.IsOpen = false;
                currentUser = loggedInUser;
                if (_userServise.IsAdmin(currentUser.Login))
                    AdminInteraction(currentUser);
                else
                    Interaction(currentUser);
            } 
            else
            {
                loginPop.IsOpen = false;
                MessageBox.Show("Невірний логін або пароль");
            }    
        }

        private void byBookBT_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            Book selectedBook = productList.SelectedItem as Book;
            if (selectedBook != null)
            {
                currentUser.UserBooks.Add(selectedBook);

                selectedBook.BookCount--;

                byProduct.ItemsSource = null;
                byProduct.ItemsSource = currentUser.UserBooks;
                productList.Items.Refresh();
                _userServise.Update(currentUser);
                _bookServise.Update(selectedBook);
            }
        }

        private void byBookBT_MouseLeftButtonUp_1(object sender, MouseButtonEventArgs e)
        {
            findNamePop.IsOpen = true;
        }

        private void Border_MouseLeftButtonUp_4(object sender, MouseButtonEventArgs e)
        {
            findNamePop.IsOpen = false;
        }

        private void Border_MouseLeftButtonUp_5(object sender, MouseButtonEventArgs e)
        {
            findAutorPop.IsOpen = false;
        }

        private void Border_MouseLeftButtonUp_6(object sender, MouseButtonEventArgs e)
        {
            findGenrePop.IsOpen = false;
        }

        private void Border_MouseLeftButtonUp_7(object sender, MouseButtonEventArgs e)
        {
            string name = findNameTB.Text;
            Book searchBook = new Book() {Name = name};
            IEnumerable<Book> foundBooks = _bookServise.Find(searchBook, FindMode.NameFinding);
            productList.ItemsSource = null;
            productList.ItemsSource = foundBooks;
        }

        private void nameSerchBT_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            findAutorPop.IsOpen = true;
        }

        private void Border_MouseLeftButtonUp_8(object sender, MouseButtonEventArgs e)
        {
            string autor = findAutorTB.Text;
            Book searchBook = new Book() { AutorLastName = autor };
            IEnumerable<Book> foundBooks = _bookServise.Find(searchBook, FindMode.AutorFinding);
            productList.ItemsSource = null;
            productList.ItemsSource = foundBooks;
        }

        private void autorSerchBT_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            findGenrePop.IsOpen = true;
        }
        private void Border_MouseLeftButtonUp_9(object sender, MouseButtonEventArgs e)
        {
            string genre = findGenrerTB.Text;
            Book searchBook = new Book() { Genre = genre };
            IEnumerable<Book> foundBooks = _bookServise.Find(searchBook, FindMode.GenreFinding);
            productList.ItemsSource = null;
            productList.ItemsSource = foundBooks;
        }

        private void autorsSerchBT_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            IEnumerable<Book> topAuthors = _bookServise.GetBooksByCriteria(BookCriteria.TopAuthors);
            productList.ItemsSource = null;
            productList.ItemsSource = topAuthors;
        }

        private void newsSerchBT_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            IEnumerable<Book> newArrivals = _bookServise.GetBooksByCriteria(BookCriteria.NewArrivals);
            productList.ItemsSource = null;
            productList.ItemsSource = newArrivals;
        }

        private void topBooksSerchBT_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            IEnumerable<Book> bestSellers = _bookServise.GetBooksByCriteria(BookCriteria.BestSellers);
            productList.ItemsSource = null;
            productList.ItemsSource = bestSellers;
        }

        private void readBookBT_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            Book selectedBook = byProduct.SelectedItem as Book; 
            if (selectedBook != null && currentUser.UserBooks.Contains(selectedBook))
            {
                BookContentWindow bookContentWindow = new BookContentWindow(selectedBook);
                bookContentWindow.Show();
            }
            else
                MessageBox.Show("Ви повинні спочатку придбати цю книгу.");
        }

        private void updateBookBT_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            Book selectedItem = (Book)productList.SelectedItem;
            _bookServise.Update(selectedItem);
        }

        private void deleteBookBT_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            Book selectedItem = (Book)productList.SelectedItem;
            _bookServise.Delete(selectedItem);
            productList.ItemsSource = null;
            productList.ItemsSource = _bookServise.GetAll();
        }

        private void Border_MouseLeftButtonUp_10(object sender, MouseButtonEventArgs e)
        {
            addPop.IsOpen= false;
        }

        private void Border_MouseLeftButtonUp_11(object sender, MouseButtonEventArgs e)
        {
            Book tmpBook = new Book()
            {
                Image = jpjTB.Text,
                Name = addNameTB.Text,
                AutorName = addAutorNameTB.Text,
                AutorLastName = addAutorLastNameTB.Text,
                Genre = addGenreTB.Text,
                CountPage = Convert.ToInt32(addCountPageTB.Text),
                BookCount = Convert.ToInt32(addBookCountTB.Text),
                Cost = Convert.ToInt32(addCostTB.Text),
                PublishingBook = new Publishing()
                {
                    Name = addPubNameTB.Text,
                    Year = Convert.ToInt32(addPubYearTB.Text)
                }
            };
            _bookServise.Add(tmpBook);
            productList.ItemsSource = null;
            productList.ItemsSource = _bookServise.GetAll();
            ClearAll();
            addPop.IsOpen = false;
        }

        private void ClearAll()
        {
            jpjTB.Clear();
            addNameTB.Clear();
            addAutorNameTB.Clear();
            addAutorLastNameTB.Clear();
            addGenreTB.Clear();
            addCountPageTB.Clear();
            addBookCountTB.Clear();
            addCostTB.Clear();
            addPubNameTB.Clear();
            addPubYearTB.Clear();
        }

        private void addBookBT_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            addPop.IsOpen= true;
        }
    }
}
