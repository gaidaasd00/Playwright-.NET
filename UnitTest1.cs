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
        const string SMSCode = "999666";
        
        //URL
        await Page.GotoAsync("https://test-jpwallet.pay2pay.tech/#/auth/steps");
        //Create 
        await Page.GetByRole(AriaRole.Textbox).ClickAsync();
        await Page.GetByRole(AriaRole.Textbox).FillAsync("+79298905138");
        await Page.GetByRole(AriaRole.Button, new() { Name = "Continue" }).ClickAsync();
        await Page.GetByRole(AriaRole.Button, new() { Name = "Create" }).ClickAsync();
        
        //sms
        await Task.Delay(3000);
        await Page.FillAsync("input[name='smsConfirmationCode']", SMSCode);
        //await Page.Keyboard.TypeAsync(SMSCode);
        
        //Info
        await Page.GetByPlaceholder("Firstname").ClickAsync();
        await Page.GetByPlaceholder("Firstname").FillAsync("asd");
        await Page.GetByPlaceholder("Lastname").ClickAsync();
        await Page.GetByPlaceholder("Lastname").FillAsync("dsa");
        await Page.GetByPlaceholder("Date of birth").ClickAsync();
        await Page.GetByPlaceholder("Date of birth").FillAsync("20.11.2000");
        await Page.GetByText("Male", new() { Exact = true }).ClickAsync();
        await Page.GetByRole(AriaRole.Button, new() { Name = "Continue" }).ClickAsync();

        //Adress
        await Page.GetByPlaceholder("Address").ClickAsync();
        await Page.GetByPlaceholder("Address").FillAsync("asd");
        await Page.GetByPlaceholder("City").ClickAsync();
        await Page.GetByPlaceholder("City").FillAsync("asd");
        await Page.GetByPlaceholder("Postal code").ClickAsync();
        await Page.GetByPlaceholder("Postal code").FillAsync("asd");
        await Page.GetByRole(AriaRole.Button, new() { Name = "Continue" }).ClickAsync();
        
        //Document
        await Page.GetByPlaceholder("Document type").ClickAsync();
        await Page.GetByRole(AriaRole.Option, new() { Name = "Permit of residence" }).ClickAsync();
        await Page.GetByPlaceholder("Number").ClickAsync();
        await Page.GetByPlaceholder("Number").FillAsync("2222");
        await Page.GetByPlaceholder("Release date").ClickAsync();
        await Page.GetByPlaceholder("Release date").FillAsync("09.01.2024");
        await Page.GetByPlaceholder("Valid to").ClickAsync();
        await Page.GetByPlaceholder("Valid to").FillAsync("09.01.2024");
        await Page.GetByRole(AriaRole.Button, new() { Name = "Continue" }).ClickAsync();
        // await Task.Delay(2000);
        //Checking that there was no transition to the next step
        var element = await Page.WaitForSelectorAsync(".h2.mb-4");
        // if (element != null) {
        //     Console.WriteLine("Остались на шаге");
        // } else {
        //     Console.WriteLine("Перешли");
        // }
        Assert.IsNotNull(element,"Осталиись на текущем шаге");

        
        
        //input
        var validationError = await Page.WaitForSelectorAsync(".app__field__input--error");
        Assert.IsNotNull(validationError, "Элемент с ошибкой отображен на странице.");

        // if (validationError != null) {
        //         Console.WriteLine("Элемент отображен на странице.");
        // //Checking for what error text appeared on the screen
        var spanElement = await Page.QuerySelectorAsync("span:has-text('Date cannot be later than ')");
        Assert.IsNotNull(spanElement, "Элемент с текстом ошибки не найден на странице.");

        // if (spanElement != null) {
        //             Console.WriteLine("Текст ошибки совпадает.");
        //         } else {
        //             Console.WriteLine("Элемент НЕ найден на странице.");
        //         }
        // Assert.IsNotNull(spanElement, "Элемент с текстом ошибки не найден на странице.");
        await Page.ScreenshotAsync(new()
        {
             Path = "/Users/alexeygaidykov/PlaywrightTests/screenshot.png",
        });

        // }
        

    }
}
