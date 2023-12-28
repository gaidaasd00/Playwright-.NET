using Microsoft.Playwright.NUnit;
using Microsoft.Playwright;

[Parallelizable(ParallelScope.Self)]
[TestFixture]
public class Tests : PageTest
{
    [Test]
    public async Task MyTest()
    {   //URL
        await Page.GotoAsync("https://test-jpwallet.pay2pay.tech/#/auth/steps");
        //Create 
        await Page.GetByRole(AriaRole.Textbox).ClickAsync();
        await Page.GetByRole(AriaRole.Textbox).FillAsync("+79250231108");
        await Page.GetByRole(AriaRole.Button, new() { Name = "Continue" }).ClickAsync();
        await Page.GetByRole(AriaRole.Button, new() { Name = "Create" }).ClickAsync();
        
        //sms
        await Task.Delay(2000);
        await Page.Keyboard.TypeAsync("999666");
        
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
        await Page.GetByPlaceholder("Release date").FillAsync("10.12.2000");
        await Page.GetByPlaceholder("Valid to").ClickAsync();
        await Page.GetByPlaceholder("Valid to").FillAsync("20.12.1998");
        await Page.GetByRole(AriaRole.Button, new() { Name = "Continue" }).ClickAsync();
        
        //input
        var validationError = await Page.WaitForSelectorAsync(".app__field__input--error");
        if (validationError != null) {
                Console.WriteLine("Элемент отображен на странице.");

        } else {
                Console.WriteLine("Элемент не отображен на странице.");

        }
        
    }
}
