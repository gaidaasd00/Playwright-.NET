using Microsoft.Playwright.NUnit;
using Microsoft.Playwright;
using System.Runtime.Intrinsics.Arm;

[Parallelizable(ParallelScope.Self)]
[TestFixture]
public class Tests : PageTest
{
    [Test]
    public async Task MyTest()
    {   
        //SMS code
        const string SMSCode = "999666";
        //Page object
        async Task ClickContinueButton()
        {
        await Page.GetByRole(AriaRole.Button, new() { Name = "Continue" }).ClickAsync();
        }
        
        //URL
        await Page.GotoAsync("https://test-jpwallet.pay2pay.tech/#/auth/steps");
        //Create 
        await Page.GetByRole(AriaRole.Textbox).ClickAsync();
        await Page.GetByRole(AriaRole.Textbox).FillAsync("+79298905138");
        await ClickContinueButton();
        await Page.GetByRole(AriaRole.Button, new() { Name = "Create" }).ClickAsync();
        
        //SMS
        await Page.FillAsync("input[name='smsConfirmationCode']", SMSCode);
        
        //Info
        await Page.GetByPlaceholder("Firstname").ClickAsync();
        await Page.GetByPlaceholder("Firstname").FillAsync("asd");
        await Page.GetByPlaceholder("Lastname").ClickAsync();
        await Page.GetByPlaceholder("Lastname").FillAsync("dsa");
        await Page.GetByPlaceholder("Date of birth").ClickAsync();
        await Page.GetByPlaceholder("Date of birth").FillAsync("20.11.2000");
        await Page.GetByText("Male", new() { Exact = true }).ClickAsync();
        await ClickContinueButton();

        //Adress
        await Page.GetByPlaceholder("Address").ClickAsync();
        await Page.GetByPlaceholder("Address").FillAsync("asd");
        await Page.GetByPlaceholder("City").ClickAsync();
        await Page.GetByPlaceholder("City").FillAsync("asd");
        await Page.GetByPlaceholder("Postal code").ClickAsync();
        await Page.GetByPlaceholder("Postal code").FillAsync("asd");
        await ClickContinueButton();
        
        //Document
        await Page.GetByPlaceholder("Document type").ClickAsync();
        await Page.GetByRole(AriaRole.Option, new() { Name = "Permit of residence" }).ClickAsync();
        await Page.GetByPlaceholder("Number").ClickAsync();
        await Page.GetByPlaceholder("Number").FillAsync("2222");
        await Page.GetByPlaceholder("Release date").ClickAsync();
        await Page.GetByPlaceholder("Release date").FillAsync("10.01.2024");
        await Page.GetByPlaceholder("Valid to").ClickAsync();
        await Page.GetByPlaceholder("Valid to").FillAsync("09.01.2024");
        await ClickContinueButton();
        await Task.Delay(5000);
        try 
        {
        // Ожидание появления элемента с текстом "Fill in your document details"
        var element = await Page.WaitForSelectorAsync(".h2.mb-4");

        // Проверка, что элемент существует
        Assert.IsNotNull(element, "Элемент с классом 'h2 mb-4' не найден на странице");

        // Получение текста из элемента
        var elementText = await element.TextContentAsync();
        Console.WriteLine($"Текст элемента: {elementText}");

        // Проверка, что текст элемента соответствует ожидаемому значению
        Assert.That(elementText, Is.EqualTo("Fill in your document details"), "Текст элемента не соответствует ожидаемому значению");
        }
        catch (AssertionException ex) 
        {
            Console.WriteLine($"не пройден {ex}");
            await Page.ScreenshotAsync(new()
            {
                Path = "/Users/alexeygaidykov/PlaywrightTests/screenshot1.png",
            });

            throw;
        }


        //ValidationError
        try 
        {
        var validationError = await Page.WaitForSelectorAsync(".app__field__input--error");
        Assert.IsTrue(validationError != null, "Элемент с ошибкой не отображен на странице.");
        Console.WriteLine($"{validationError}");
        } 
        catch (AssertionException ex) {
            Console.WriteLine($"Провален {ex}");
            await Page.ScreenshotAsync(new()
        {
             Path = "/Users/alexeygaidykov/PlaywrightTests/screenshot2.png",
        });

        throw;
        }
        
        
        //Checking for what error text appeared on the screen
        try 
        {

        var spanElement = await Page.QuerySelectorAsync("span:has-text('Date cannot be later than ')");
        Assert.IsTrue(spanElement != null, "Элемент с текстом ошибки не найден на странице.");
        Console.WriteLine($"{spanElement}");

        }
        catch (AssertionException ex) {
            Console.WriteLine($"Тест провален {ex.Message}");
             await Page.ScreenshotAsync(new()
        {
             Path = "/Users/alexeygaidykov/PlaywrightTests/screenshot3.png",
        });

        throw;
        }
    }
}
