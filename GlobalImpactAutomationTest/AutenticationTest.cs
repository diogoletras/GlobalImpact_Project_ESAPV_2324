using OpenQA.Selenium.Chrome;
using OpenQA.Selenium;

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

            username.SendKeys("testuser");
            password.SendKeys("!Qq1234");

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

            username.SendKeys("testuser");
            password.SendKeys("!Qq1234");

            driver.FindElement(By.Id("login")).Click();

            driver.FindElement(By.Id("logout")).Click();

            driver.Quit();
        }
    }
}