using OpenQA.Selenium.Chrome;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.ObjectModel;

namespace GlobalImpactAutomationTest
{
    public class AutenticationTest
    {
        private ChromeDriver driver;

        public AutenticationTest()
        {
            driver = new ChromeDriver();
        }

        [Fact]
        public void Register_Automation_Test()
        {
            driver.Url = "https://localhost:7154";
            driver.FindElement(By.LinkText("REGISTER")).Click();

            var username = driver.FindElement(By.Id("UserName"));
            var firstname = driver.FindElement(By.Id("FirstName"));
            var lastname = driver.FindElement(By.Id("LastName"));
            var email = driver.FindElement(By.Id("Email"));
            var age = driver.FindElement(By.Id("Age"));
            var nif = driver.FindElement(By.Id("NIF"));
            var password = driver.FindElement(By.Id("Password"));
            var confirmPassword = driver.FindElement(By.Id("ConfirmPassword"));


            username.SendKeys("testuser");
            firstname.SendKeys("test");
            lastname.SendKeys("user");
            email.SendKeys("testemail@gmail.com");
            age.SendKeys("20");
            nif.SendKeys("123456789");
            password.SendKeys("!Qq1234");
            confirmPassword.SendKeys("!Qq1234");

            driver.FindElement(By.Id("register")).Click();

            driver.Quit();
        }

        [Fact]
        public void Login_Automation_Test()
        {
            driver.Url = "https://localhost:7154";
            driver.FindElement(By.LinkText("LOGIN")).Click();

            var username = driver.FindElement(By.Id("username"));
            var password = driver.FindElement(By.Id("password"));

            username.SendKeys("cliente");

            password.SendKeys("Cliente123");

            driver.FindElement(By.Id("login")).Click();

            driver.Quit();
        }

        [Fact]
        public void Logout_Automation_Test()
        {
            driver.Url = "https://localhost:7154";
            driver.FindElement(By.LinkText("LOGIN")).Click();

            var username = driver.FindElement(By.Id("username"));
            var password = driver.FindElement(By.Id("password"));

            username.SendKeys("cliente");

            password.SendKeys("Cliente123");

            driver.FindElement(By.Id("login")).Click();

            driver.FindElement(By.Id("logout")).Click();

            driver.Quit();
        }

        [Fact]
        public void UserPage_Automation_Test()
        {
            driver.Url = "https://localhost:7154";
            driver.FindElement(By.LinkText("LOGIN")).Click();

            var username = driver.FindElement(By.Id("username"));
            var password = driver.FindElement(By.Id("password"));

            username.SendKeys("cliente");
            
            password.SendKeys("Cliente123");

            driver.FindElement(By.Id("login")).Click();

            Thread.Sleep(2000);

            driver.FindElement(By.LinkText("Perfil")).Click();

            Thread.Sleep(2000);
            //driver.FindElement(By.Id("logout")).Click();

            driver.Quit();
        }

        [Fact]
        public void Recycle_Automation_Test()
        {
            driver.Url = "https://localhost:7154";
            driver.FindElement(By.LinkText("ECOPONTO")).Click();

            Thread.Sleep(2000);

            // Encontrar e clicar no primeiro botão "Submeter"
            var submitButton = driver.FindElement(By.CssSelector(".btn-grad"));
            submitButton.Click();

            Thread.Sleep(2000);

            var uniqueCode = driver.FindElement(By.Id("uniqueCode"));
            uniqueCode.SendKeys("5cbb3168-81b2-41fc-995c-c8193beb2773");

            Thread.Sleep(2000);

			var submitButton2 = driver.FindElement(By.CssSelector(".btn-grad"));
			submitButton2.Click();

			Thread.Sleep(2000);

			ReadOnlyCollection<IWebElement> checkoutButtons = driver.FindElements(By.CssSelector(".btn-grad-small"));
			checkoutButtons[2].Click();
			Thread.Sleep(2000);

            ReadOnlyCollection<IWebElement> checkoutButtons2 = driver.FindElements(By.CssSelector(".btn-grad-small"));
            checkoutButtons2[2].Click();
            Thread.Sleep(2000);

            var submitButton3 = driver.FindElement(By.CssSelector(".btn-grad"));
			submitButton3.Click();

			Thread.Sleep(2000);
			driver.Quit();
		}

        [Fact]
		public void Store_Automation_Test()
		{
			driver.Url = "https://localhost:7154";
			driver.FindElement(By.LinkText("LOGIN")).Click();

			var username = driver.FindElement(By.Id("username"));
			var password = driver.FindElement(By.Id("password"));

            username.SendKeys("cliente");

            password.SendKeys("Cliente123");

            driver.FindElement(By.Id("login")).Click();

			Thread.Sleep(2000);

			driver.FindElement(By.LinkText("Loja Virtual")).Click();

			Thread.Sleep(2000);

			ReadOnlyCollection<IWebElement> checkoutButtons = driver.FindElements(By.CssSelector(".btn-primary"));
            checkoutButtons[1].Click();
			Thread.Sleep(2000);

            ReadOnlyCollection<IWebElement> checkoutButtons2 = driver.FindElements(By.CssSelector(".btn-primary"));
            checkoutButtons2[2].Click();
            Thread.Sleep(2000);

            ReadOnlyCollection<IWebElement> checkoutButtons3 = driver.FindElements(By.CssSelector(".btn-primary"));
            checkoutButtons3[3].Click();
            Thread.Sleep(2000);


            ReadOnlyCollection<IWebElement> checkoutButtons4 = driver.FindElements(By.CssSelector(".btn-primary"));
			checkoutButtons4[0].Click();
			Thread.Sleep(2000);

			IWebElement checkoutButton = driver.FindElement(By.CssSelector(".btn-primary"));
			checkoutButton.Click();

			Thread.Sleep(2000);

			//driver.FindElement(By.Id("logout")).Click();
			driver.Quit();
		}

        [Fact]
        public void LoginRecicle_Automation_Test()
        {
            driver.Url = "https://localhost:7154";
            driver.FindElement(By.LinkText("LOGIN")).Click();

            var username = driver.FindElement(By.Id("username"));
            var password = driver.FindElement(By.Id("password"));

            username.SendKeys("cliente");
            password.SendKeys("Cliente123");

            driver.FindElement(By.Id("login")).Click();

            Thread.Sleep(2000);

            driver.FindElement(By.LinkText("Perfil")).Click();

            Thread.Sleep(2000);
            driver.FindElement(By.Id("logout")).Click();

            Thread.Sleep(2000);

            driver.FindElement(By.LinkText("ECOPONTO")).Click();

            Thread.Sleep(2000);

            // Encontrar e clicar no primeiro botão "Submeter"
            var submitButton = driver.FindElement(By.CssSelector(".btn-grad"));
            submitButton.Click();

            Thread.Sleep(2000);

            var uniqueCode = driver.FindElement(By.Id("uniqueCode"));
            uniqueCode.SendKeys("5cbb3168-81b2-41fc-995c-c8193beb2773");

            Thread.Sleep(2000);

            var submitButton2 = driver.FindElement(By.CssSelector(".btn-grad"));
            submitButton2.Click();

            Thread.Sleep(2000);

            ReadOnlyCollection<IWebElement> checkoutButtons = driver.FindElements(By.CssSelector(".btn-grad-small"));
            checkoutButtons[2].Click();
            Thread.Sleep(2000);

            ReadOnlyCollection<IWebElement> checkoutButtons2 = driver.FindElements(By.CssSelector(".btn-grad-small"));
            checkoutButtons2[2].Click();
            Thread.Sleep(2000);

            var submitButton3 = driver.FindElement(By.CssSelector(".btn-grad"));
            submitButton3.Click();

            Thread.Sleep(2000);

            driver.FindElement(By.Id("home")).Click();

            driver.FindElement(By.LinkText("LOGIN")).Click();

            var username2 = driver.FindElement(By.Id("username"));
            var password2 = driver.FindElement(By.Id("password"));

            username2.SendKeys("cliente");
            password2.SendKeys("Cliente123");

            driver.FindElement(By.Id("login")).Click();

            Thread.Sleep(2000);

            driver.FindElement(By.LinkText("Perfil")).Click();

            Thread.Sleep(2000);
            driver.FindElement(By.Id("logout")).Click();

            Thread.Sleep(2000);

            driver.Quit();
        }

        [Fact]
        public void CancelDelivery_Automation_Test()
        {
			driver.Url = "https://localhost:7154";
			driver.FindElement(By.LinkText("LOGIN")).Click();

			var username = driver.FindElement(By.Id("username"));
			var password = driver.FindElement(By.Id("password"));

			username.SendKeys("cliente");

			password.SendKeys("Cliente123");

			driver.FindElement(By.Id("login")).Click();

			Thread.Sleep(2000);

			driver.FindElement(By.LinkText("Loja Virtual")).Click();

			Thread.Sleep(2000);

			ReadOnlyCollection<IWebElement> checkoutButtons = driver.FindElements(By.CssSelector(".btn-primary"));
			checkoutButtons[1].Click();
			Thread.Sleep(2000);

			ReadOnlyCollection<IWebElement> checkoutButtons2 = driver.FindElements(By.CssSelector(".btn-primary"));
			checkoutButtons2[2].Click();
			Thread.Sleep(2000);

			ReadOnlyCollection<IWebElement> checkoutButtons3 = driver.FindElements(By.CssSelector(".btn-primary"));
			checkoutButtons3[3].Click();
			Thread.Sleep(2000);


			ReadOnlyCollection<IWebElement> checkoutButtons4 = driver.FindElements(By.CssSelector(".btn-primary"));
			checkoutButtons4[0].Click();
			Thread.Sleep(2000);

			IWebElement checkoutButton = driver.FindElement(By.CssSelector(".btn-primary"));
			checkoutButton.Click();

			Thread.Sleep(2000);

			driver.FindElement(By.LinkText("Minhas Encomendas")).Click();

			Thread.Sleep(2000);

			IWebElement checkoutButton2 = driver.FindElement(By.CssSelector(".btn-danger"));
			checkoutButton2.Click();

			Thread.Sleep(2000);

			IWebElement checkoutButton3 = driver.FindElement(By.CssSelector(".btn-danger"));
			checkoutButton3.Click();

			Thread.Sleep(2000);

			driver.Quit();
		}

        [Fact]
        public void ConfirmDelivery_Automation_Test()
        {
			driver.Url = "https://localhost:7154";
			driver.FindElement(By.LinkText("LOGIN")).Click();

			var username = driver.FindElement(By.Id("username"));
			var password = driver.FindElement(By.Id("password"));

			username.SendKeys("cliente");

			password.SendKeys("Cliente123");

			driver.FindElement(By.Id("login")).Click();

			Thread.Sleep(2000);

			driver.FindElement(By.LinkText("Loja Virtual")).Click();

			Thread.Sleep(2000);

			ReadOnlyCollection<IWebElement> checkoutButtons = driver.FindElements(By.CssSelector(".btn-primary"));
			checkoutButtons[1].Click();
			Thread.Sleep(2000);

			ReadOnlyCollection<IWebElement> checkoutButtons2 = driver.FindElements(By.CssSelector(".btn-primary"));
			checkoutButtons2[2].Click();
			Thread.Sleep(2000);

			ReadOnlyCollection<IWebElement> checkoutButtons3 = driver.FindElements(By.CssSelector(".btn-primary"));
			checkoutButtons3[3].Click();
			Thread.Sleep(2000);


			ReadOnlyCollection<IWebElement> checkoutButtons4 = driver.FindElements(By.CssSelector(".btn-primary"));
			checkoutButtons4[0].Click();
			Thread.Sleep(2000);

			IWebElement checkoutButton = driver.FindElement(By.CssSelector(".btn-primary"));
			checkoutButton.Click();

			Thread.Sleep(2000);

			driver.FindElement(By.Id("logout")).Click();

			Thread.Sleep(2000);

			driver.FindElement(By.LinkText("LOGIN")).Click();

			var username2 = driver.FindElement(By.Id("username"));
			var password2 = driver.FindElement(By.Id("password"));

			username2.SendKeys("admin");

			password2.SendKeys("Admin123");

			driver.FindElement(By.Id("login")).Click();

			Thread.Sleep(2000);

			driver.FindElement(By.LinkText("Encomendas")).Click();

			Thread.Sleep(2000);

			IWebElement checkoutButton2 = driver.FindElement(By.CssSelector(".btn-success"));
			checkoutButton2.Click();

			Thread.Sleep(2000);

			IWebElement checkoutButton3 = driver.FindElement(By.CssSelector(".btn-success"));
			checkoutButton3.Click();

			Thread.Sleep(2000);

			driver.FindElement(By.Id("logout")).Click();

			Thread.Sleep(2000);

			driver.FindElement(By.LinkText("LOGIN")).Click();

			var username3 = driver.FindElement(By.Id("username"));
			var password3 = driver.FindElement(By.Id("password"));

			username3.SendKeys("cliente");

			password3.SendKeys("Cliente123");

			driver.FindElement(By.Id("login")).Click();

			Thread.Sleep(2000);

			driver.FindElement(By.LinkText("Minhas Encomendas")).Click();

			Thread.Sleep(2000);

			driver.Quit();

		}


	}
}