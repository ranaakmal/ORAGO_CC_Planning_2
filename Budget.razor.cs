using System;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using ORAGO_CC_Planning.Data;
using ORAGO_CC_Planning.Models;
using Syncfusion.Blazor.DropDowns;
using Syncfusion.Blazor.Grids;
using Syncfusion.Blazor.Inputs;


namespace ORAGO_CC_Planning.Pages
{
    public class BudgetBase : ComponentBase
    {
        [Inject]
        AuthenticationStateProvider? authenticationStateProvider { get; set; }
        [Inject]
        IArticleFinalRepository? ArticleService { get; set; }
        [Inject]
        ICurrencyRepository? CurrencyService { get; set; }
        [Inject]
        ICustomerFinalRepository? CustomerService { get; set; }
        [Inject]
        IExchangeRateRepository? ExchangeRateService { get; set; }
        [Inject]
        ITransactionRepository? TransactionService { get; set; }
        [Inject]
        IBudgetEntryRepository? BudgetEntryService { get; set; }
        [Inject]
        IYearlyLockRepository? YearlyLockService { get; set; }
        [Inject]
        IBudgetCurrencyRepository? BudgetCurrencyService { get; set; }
        [Inject]
        IVolumeTemplateRepository? VolumeTemplateService { get; set; }
        [Inject]
        IEntityLocalCurrencyRepository? EntityLocalCurrencyService { get; set; }
        [Parameter]
        public int year { get; set; }
        [Parameter]
        public int artiId { get; set; }
        [Parameter]
        public int customId { get; set; }
        public bool IsAdd { get; set; }
        public bool enabled { get; set; } = false;
        protected bool? isChecked { get; set; } = null;
        protected int budgetStartYear {get; set; } = DateTime.Now.Year + 1;
        protected int actualDataYear { get; set; } = DateTime.Now.Year - 1;
        protected List<CustomerFinal>? customers;
        protected CustomerFinal? customer;
        protected int customerId;
        protected List<ArticleFinal>? articles;
        protected ArticleFinal? article;
        protected int articleId;
        protected List<ActualDataEntry>? actualDataEntries;
        protected List<BudgetEntry>? budgetEntries;
        protected BudgetEntry? budgetEntry;
        protected int budgetEntryId;
        protected List<YearlyLock>? yearlyLocks;
        protected int yearlyLockId;
        protected YearlyLock? yearlyLock;
        protected int activeForecast = 1;
        protected List<ActualDataEntry>? fC1ActualDataEntries;
        protected List<ActualDataEntry>? fC2ActualDataEntries;
        protected List<ActualDataEntry>? fC3ActualDataEntries;
        protected BudgetCurrency? budgetCurrency;
        protected List<BudgetCurrency>? budgetCurrencies;
        protected EntityLocalCurrency? entityLocalCurrency;
        protected int entityLocalCurrencyId;
        protected List<EntityLocalCurrency>? entityLocalCurrencies;
        protected double annualQuantity;
        protected double defaultPrice;
        protected double defaultSurcharge;
        protected double annualQuantityNextYear;
        protected double defaultPriceNextYear;
        protected double defaultSurchargeNextYear;
        protected double annualQuantityForecast;
        protected double annualQuantityForecastNextYear;
        protected double defaultPriceForecast;
        protected double defaultSurchargeForecast;
        protected double annualQuantity2Forecast;
        protected double defaultPrice2Forecast;
        protected double defaultSurcharge2Forecast;
        protected double annualQuantity3Forecast;
        protected double defaultPrice3Forecast;
        protected double defaultSurcharge3Forecast;
        protected List<Currency>? currencies;
        protected ExchangeRate? exchangeRate;
        protected int exchangeRateId;
        public List<ExchangeRate>? exchangeRates;   //needs to be accessed from within grids therefore public
        protected string displayCurrency = "Document currency";
        public double xRDL0 = 1;    //Document currency to Local currency year 1
        public double xRDE0 = 1;    //Document currency to EUR year 1
        public double xRDL1 = 1;    //Document currency to Local currency year 1
        public double xRDE1 = 1;    //Document currency to EUR year 1
        public double xRDL2 = 1;    //Document currency to Local currency year 1
        public double xRDE2 = 1;    //Document currency to EUR year 1
        public VolumeTemplate? volumeTemplate;
        public int volumeTemplateId;
        public List<VolumeTemplate>? volumeTemplates;
        public List<string>? dbwEntities;
        public string? dbwEntity;
        public IEditorSettings NumericEditParams = new NumericEditCellParams
        {
            Params = new NumericTextBoxModel<object>() { ShowClearButton = false, ShowSpinButton = false }
        };
        protected bool ShowDocumentValues
        {
            get
            {
                if (budgetCurrency is not null)
                {
                    return displayCurrency == "Document currency";
                }
                return false;
            }
        }
        protected bool ShowLocalValues
        {
            get
            {
                if (budgetCurrency is not null)
                {
                    return displayCurrency == "Local currency";
                }
                return false;
            }
        }
        protected bool ShowEurValues
        {
            get
            {
                if (budgetCurrency is not null)
                {
                    return displayCurrency == "Euro";
                }
                return false;
            }
        }

        public class ActualDataEntry
        {
            public ActualDataEntry(int _yearName, int _durationNr)
            {
                this.DurationNr = _durationNr;
                if (this.DurationNr > 24)
                {
                    this.MonthName = DateTime.Parse("2000, " + (this.DurationNr - 24).ToString() + ", 1").ToString("MMM");
                    this.MonthNr = DurationNr - 24;
                    this.YearName = _yearName + 2;
                }
                else if (this.DurationNr > 12)
                {
                    this.MonthName = DateTime.Parse("2000, " + (this.DurationNr - 12).ToString() + ", 1").ToString("MMM");
                    this.MonthNr = DurationNr - 12;
                    this.YearName = _yearName + 1;
                }
                else
                {
                    this.MonthName = DateTime.Parse("2000, " + this.DurationNr.ToString() + ", 1").ToString("MMM");
                    this.MonthNr = DurationNr;
                    this.YearName = _yearName;
                }
            }
            public int DurationNr { get; set; }
            public int MonthNr { get; set; }
            public string? MonthName { get; set; }
            public int YearName { get; set; }
            public double Quantity { get; set; }
            public double SalesLC { get; set; }
            public double SurchargeLC { get; set; }
            public double TotalPriceLC
            {
                get
                {
                    if (this.Quantity != 0) return SalesLC / Quantity;
                    return 0;
                }
                set { }
            }
            public double NetPriceLC
            {
                get
                {
                    return this.TotalPriceLC - this.SurchargeLC;
                }
                set { }
            }
            public double SalesDC { get; set; }
            public double SurchargeDC { get; set; }
            public double TotalPriceDC
            {
                get
                {
                    if (this.Quantity != 0) return SalesDC / Quantity;
                    return 0;
                }
                set { }
            }
            public double NetPriceDC
            {
                get
                {
                    return this.TotalPriceLC - this.SurchargeLC;
                }
                set { }
            }
            public string CurrencyDc {get; set;}
            public string CurrencyLc {get; set;}
            public double PriceEur {get; set;}
            public double SalesEur {get; set;}
            public double SurchargeEur { get; set; }
            public double TotalPriceEur
            {
                get
                {
                    if (this.Quantity != 0) return SalesEur / Quantity;
                    return 0;
                }
                set { }
            }
            public double NetPriceEur
            {
                get
                {
                    return this.TotalPriceEur - this.SurchargeEur;
                }
                set { }
            }
        } 
        protected SfGrid<BudgetEntry>? Grid {get; set;}
        protected List<ArticleFinal>? userArticles;
        protected int countTotalArticles;
        protected int countCompleteArticles;
        protected int countIncompleteAeticles;
        protected List<BudgetEntry>? userBudgetEntries;
        protected List<BudgetEntry>? interestingBudgetEntries;
        protected List<BudgetEntry>? completedBudgetEntries;
        protected List<BudgetEntry>? incompleteBudgetEntries;
        private BudgetEntry? latestBudgetEntry;
        private List<BudgetEntry>? LatestBudgetEntries;
        protected bool VisibleSpinner { get; set; } = false;
        protected override async Task OnInitializedAsync()
        {
            this.VisibleSpinner = true;
            await Task.Delay(1000);
            this.VisibleSpinner = false;
            dbwEntities = await CustomerService!.GetEntitiesListAsync();
            yearlyLocks = await YearlyLockService!.GetByYearAsync(budgetStartYear);
            if (yearlyLocks is not null) yearlyLock = yearlyLocks.Where(l => l.IsLocked == false).FirstOrDefault();
            if (yearlyLock is not null) yearlyLockId = yearlyLock.Id;
            customers = await CustomerService!.GetAllAsync();
            customer = await CustomerService.GetByIdAsync(customerId);
            articles = await ArticleService!.GetAllAsync();
            article = await ArticleService!.GetByIdAsync(articleId);
            userArticles = await ArticleService.GetAllAsync();
            currencies = await CurrencyService!.GetAllAsync();
            volumeTemplates = await VolumeTemplateService!.GetAllAsync();
            volumeTemplate = volumeTemplates.FirstOrDefault();
            if (volumeTemplate is not null) volumeTemplateId = volumeTemplate.Id;
            entityLocalCurrencies = await EntityLocalCurrencyService!.GetAllAsync();
            //budgetEntries = await BudgetEntryService!.GetAllAsync();
            budgetEntry = await BudgetEntryService!.GetByIdAsync(budgetEntryId);        
            await FetchUserCustomerArticles(customer);
            exchangeRates = await ExchangeRateService!.GetAllAsync();
            activeForecast = yearlyLock?.ActiveForecast ?? 1;
            
            if (customId != 0 && artiId != 0)
            {
                this.VisibleSpinner = true;
                await Task.Delay(1000);
                this.VisibleSpinner = false;
                //articles = await ArticleService!.GetAllAsync();
                article = await ArticleService!.GetByIdAsync(artiId);
                articles.Add(article);
                // article = articles.Find(a => a.Id.Equals(artiId));
                customer = customers.Find(c => c.Id.Equals(customId));
                entityLocalCurrencies = await EntityLocalCurrencyService!.GetAllAsync();
                dbwEntity = article?.Entity;
                customerId = customer?.Id ?? 0;
                articleId = article?.Id ?? 0;
                await PopulateActualDataAsync();
                if (customerId != 0 && articleId != 0 && customer is not null && article is not null)
                {
                    budgetEntries = await BudgetEntryService!.GetByYearVersionCustomerArticleAsync(budgetStartYear, article, customer, yearlyLock?.Name, authenticationStateProvider);
                    budgetCurrency = await BudgetCurrencyService!.GetByYearVersionCustomerArticleAsync(budgetStartYear, article, customer, yearlyLock?.Name);
                    budgetCurrency.LocalCurrencyCode = (entityLocalCurrencies?.Where(c => c.Entity == dbwEntity).FirstOrDefault()?.LocalCurrencyCode) ?? "";
                    budgetCurrency.DocumentCurrencyCode = (entityLocalCurrencies?.Where(c => c.Entity == dbwEntity).FirstOrDefault()?.DocumentCurrencyCode) ?? "";
                    await FetchUserCustomerArticles(customer);
                    // if(budgetCurrency.DocumentCurrencyCode is null && budgetCurrency.DocumentCurrencyCode == "")
                    // {
                        //budgetCurrency.DocumentCurrencyCode = (entityLocalCurrencies?.Where(c => c.Entity == dbwEntity).FirstOrDefault()?.DocumentCurrencyCode) ?? "";
                    // }
                    await budgetCurrency.SetModifyInfo(authenticationStateProvider!);
                    await BudgetCurrencyService.UpdateEntityAsync(budgetCurrency);
                    await PopulateFC1ActualDataAsync();
                    await PopulateFC2ActualDataAsync();
                    await PopulateFC3ActualDataAsync();
                    await LoadCurrencyInfoAsync();
                }
            }
        }  
        protected async Task FetchUserCustomerArticles(CustomerFinal givenCustomer)
        {
            userArticles = new();
            interestingBudgetEntries = new();
            userBudgetEntries = new();
            completedBudgetEntries = new();
            incompleteBudgetEntries = new();
            countCompleteArticles = 0;
            countIncompleteAeticles = 0;
            countTotalArticles = 0;
            entityLocalCurrencies = new();
            //budgetEntries = new();
                if ((givenCustomer.BpCode is not null) && (givenCustomer.Id != 0))
                {
                    List<String?> articleNames = await TransactionService!.GetArticleNamesByCustomerAsync(givenCustomer.BpCode);
                    foreach (string? articleName in articleNames)
                    {
                        if (!ArticleService!.SingleLoading)
                        {
                            ArticleFinal article = await ArticleService!.GetByItemNrAsync(articleName);
                            if ((article is not null) && (article.Id != 0))
                            {
                                userArticles.Add(article);
                                List<BudgetEntry> itsBudgetEntries = await BudgetEntryService!.GetByYearVersionCustomerArticleForIndexAsync(budgetStartYear,
                                article, givenCustomer, yearlyLock?.Name);
                                itsBudgetEntries = itsBudgetEntries.Where(e => e.DurationNumber == 13).ToList();
                                userBudgetEntries?.AddRange(itsBudgetEntries);
                                BudgetCurrency itsBudgetCurrency = await BudgetCurrencyService!.GetByYearVersionCustomerArticleAsync(budgetStartYear,
                                article, givenCustomer, yearlyLock?.Name);
                                if (itsBudgetCurrency.PlanningComplete)
                                {
                                    countCompleteArticles++;
                                    completedBudgetEntries.AddRange(itsBudgetEntries);
                                }
                                else
                                {
                                    countIncompleteAeticles++;
                                    incompleteBudgetEntries.AddRange(itsBudgetEntries);
                                }
                            }
                        }
                    }
                }
            interestingBudgetEntries = userBudgetEntries;
            countTotalArticles = userArticles.Count();
        }
        protected async Task CustomerSelected(ChangeEventArgs<int, CustomerFinal> args)
        {
            article = new();
            articles = new();
            budgetEntries = new();
            actualDataEntries = new();
            fC1ActualDataEntries = new();
            fC2ActualDataEntries = new();
            fC3ActualDataEntries = new();
            budgetCurrency = new();
            completedBudgetEntries = new();
            incompleteBudgetEntries = new();
            countCompleteArticles = 0;
            countIncompleteAeticles = 0;
            countTotalArticles = 0;
            if ((!CustomerService!.SingleLoading) && (customerId != 0))
            {
                customer = await CustomerService.GetFullByIdAsync(customerId);
            }
            if (customer is not null && customer.BpCode is not null)
            {
                ArticleService!.ListLoading = true;
                 List<String?> articleNames = await TransactionService!.GetArticleNamesByCustomerAsync(customer.BpCode);
                articles = new();
                foreach (string? articleName in articleNames)
                {
                    ArticleFinal article = await ArticleService.GetByItemNrAsync(articleName);
                    if ((article is not null) && (article.Id != 0))
                    {
                        articles.Add(article);
                        List<BudgetEntry> itsBudgetEntries = await BudgetEntryService!.GetByYearVersionCustomerArticleForIndexAsync(budgetStartYear,
                        article, customer, yearlyLock?.Name);
                        itsBudgetEntries = itsBudgetEntries.Where(e => e.DurationNumber == 13).ToList();
                        userBudgetEntries?.AddRange(itsBudgetEntries);
                        BudgetCurrency itsBudgetCurrency = await BudgetCurrencyService!.GetByYearVersionCustomerArticleAsync(budgetStartYear,
                        article, customer, yearlyLock?.Name);
                        if (itsBudgetCurrency.PlanningComplete)
                        {
                            countCompleteArticles++;
                            completedBudgetEntries.AddRange(itsBudgetEntries);
                        }
                        else
                        {
                            countIncompleteAeticles++;
                            incompleteBudgetEntries.AddRange(itsBudgetEntries);
                        }
                    }
                }
                countTotalArticles = articles.Count();
                ArticleService.ListLoading = false;
                List<ArticleFinal>? dummyArticles = await ArticleService.GetDummyByCustomerAsync(customer.BpCode);
                if (dummyArticles is not null) articles.AddRange(dummyArticles);
            }
        }

        protected async Task EntitySelected(ChangeEventArgs<string, string> args)
        {
            if (!CustomerService!.ListLoading) customers = await CustomerService.GetByLoggedInUserAndEntity(authenticationStateProvider!, dbwEntity);
            if(customer is null && article is null)
            {
            customer = new();
            article = new();
            budgetEntries = new();
            actualDataEntries = new();
            fC1ActualDataEntries = new();
            fC2ActualDataEntries = new();
            fC3ActualDataEntries = new();
            budgetCurrency = new();
            }
        }

        protected async Task ArticleSelected(ChangeEventArgs<int, ArticleFinal> args)
        {
            budgetCurrency = new();
            budgetEntries = new();
            annualQuantityForecast = new();
            defaultPriceForecast = new();
            defaultSurchargeForecast = new();
            annualQuantity2Forecast = new();
            defaultPrice2Forecast = new();
            defaultSurcharge2Forecast = new();
            annualQuantity3Forecast = new();
            defaultPrice3Forecast = new();
            defaultSurcharge3Forecast = new();
            annualQuantity = new();
            defaultPrice = new();
            defaultSurcharge = new();
            annualQuantityNextYear = new();
            defaultPriceNextYear = new();
            defaultSurchargeNextYear = new();
            if ((!ArticleService!.SingleLoading) && (articleId != 0))
            {
                article = await ArticleService.GetFullByIdAsync(articleId);
                await PopulateActualDataAsync();
                if (!BudgetEntryService!.ListLoading && customer is not null && article is not null && yearlyLock is not null)
                {
                    budgetEntries = await BudgetEntryService.GetByYearVersionCustomerArticleAsync(budgetStartYear, article, customer, yearlyLock.Name, authenticationStateProvider);
                    budgetCurrency = await BudgetCurrencyService!.GetByYearVersionCustomerArticleAsync(budgetStartYear, article, customer, yearlyLock.Name);
                    await PopulateFC1ActualDataAsync();
                    await PopulateFC2ActualDataAsync();
                    await PopulateFC3ActualDataAsync();
                    await LoadCurrencyInfoAsync();
                }
            }
        }
    protected async Task TotalArticlesClicked()
    {
        article = new();
        articleId = new();
        userArticles = new();
        budgetEntries = new();
        actualDataEntries = new();
        fC1ActualDataEntries = new();
        fC2ActualDataEntries = new();
        fC3ActualDataEntries = new();
        budgetCurrency = new();
        interestingBudgetEntries = new();
        userBudgetEntries = new();
        StateHasChanged(); 
        if ((!CustomerService!.SingleLoading) && (customerId != 0))
        {
            customer = await CustomerService.GetFullByIdAsync(customerId);
        }
        if (customer is not null && customer.BpCode is not null)
        {
            ArticleService!.ListLoading = true; 
            List<String?> articleNames = await TransactionService!.GetArticleNamesByCustomerAsync(customer.BpCode);
            articles = new();
            foreach (string? articleName in articleNames)
            {
                ArticleFinal article = await ArticleService.GetByItemNrAsync(articleName);
                if ((article is not null) && (article.Id != 0))
                {
                    //userArticles.Add(article);
                    latestBudgetEntry = await BudgetEntryService!.GetLatestByLoggedInUser(authenticationStateProvider);
                    LatestBudgetEntries = await BudgetEntryService.GetLatestArticlesByLoggedInUser(authenticationStateProvider);
                    List<BudgetEntry> itsBudgetEntries = await
                    BudgetEntryService.GetByYearVersionCustomerArticleForIndexAsync(budgetStartYear,
                    article, customer, yearlyLock?.Name);
                    itsBudgetEntries = itsBudgetEntries.Where(e => e.DurationNumber == 13).ToList();
                    userBudgetEntries?.AddRange(itsBudgetEntries);
                    BudgetCurrency itsBudgetCurrency = await BudgetCurrencyService!.GetByYearVersionCustomerArticleAsync(budgetStartYear,
                    article, customer, yearlyLock?.Name);
                    if (article is not null) articles.Add(article);
                }
            }
        }
    }
    protected async Task CompletedArticlesClicked()
    {
        article = new();
        articleId = new();
        userArticles = new();
        budgetEntries = new();
        actualDataEntries = new();
        fC1ActualDataEntries = new();
        fC2ActualDataEntries = new();
        fC3ActualDataEntries = new();
        budgetCurrency = new();
        interestingBudgetEntries = new();
        userBudgetEntries = new();
        StateHasChanged();
        if ((!CustomerService!.SingleLoading) && (customerId != 0))
        {
            customer = await CustomerService.GetFullByIdAsync(customerId);
        }
        if (customer is not null && customer.BpCode is not null)
        {
            ArticleService!.ListLoading = true; 
            List<String?> articleNames = await TransactionService!.GetArticleNamesByCustomerAsync(customer.BpCode);
            articles = new();
            foreach (string? articleName in articleNames)
            {
                ArticleFinal article = await ArticleService.GetByItemNrAsync(articleName);
                if ((article is not null) && (article.Id != 0))
                {
                    //userArticles.Add(article);
                    latestBudgetEntry = await BudgetEntryService!.GetLatestByLoggedInUser(authenticationStateProvider);
                    LatestBudgetEntries = await BudgetEntryService.GetLatestArticlesByLoggedInUser(authenticationStateProvider);
                    List<BudgetEntry> itsBudgetEntries = await
                    BudgetEntryService.GetByYearVersionCustomerArticleForIndexAsync(budgetStartYear,
                    article, customer, yearlyLock?.Name);
                    itsBudgetEntries = itsBudgetEntries.Where(e => e.DurationNumber == 13).ToList();
                    userBudgetEntries?.AddRange(itsBudgetEntries);
                    BudgetCurrency itsBudgetCurrency = await BudgetCurrencyService!.GetByYearVersionCustomerArticleAsync(budgetStartYear,
                    article, customer, yearlyLock?.Name);
                    if (itsBudgetCurrency!.PlanningComplete)
                    {
                        if (article is not null) articles.Add(article);
                    }
                }
            }
        }
    }
    protected async Task InCompletedArticlesClicked()
    {
        article = new();
        articleId = new();
        userArticles = new();
        budgetEntries = new();
        actualDataEntries = new();
        fC1ActualDataEntries = new();
        fC2ActualDataEntries = new();
        fC3ActualDataEntries = new();
        budgetCurrency = new();
        interestingBudgetEntries = new();
        userBudgetEntries = new();
        StateHasChanged();
        if ((!CustomerService!.SingleLoading) && (customerId != 0))
        {
            customer = await CustomerService.GetFullByIdAsync(customerId);
        }
        if (customer is not null && customer.BpCode is not null)
        {
            ArticleService!.ListLoading = true; 
            List<String?> articleNames = await TransactionService!.GetArticleNamesByCustomerAsync(customer.BpCode);
            articles = new();
            foreach (string? articleName in articleNames)
            {
                ArticleFinal article = await ArticleService.GetByItemNrAsync(articleName);
                if ((article is not null) && (article.Id != 0))
                {
                    //userArticles.Add(article);
                    latestBudgetEntry = await BudgetEntryService!.GetLatestByLoggedInUser(authenticationStateProvider);
                    LatestBudgetEntries = await BudgetEntryService.GetLatestArticlesByLoggedInUser(authenticationStateProvider);
                    List<BudgetEntry> itsBudgetEntries = await
                    BudgetEntryService.GetByYearVersionCustomerArticleForIndexAsync(budgetStartYear,
                    article, customer, yearlyLock?.Name);
                    itsBudgetEntries = itsBudgetEntries.Where(e => e.DurationNumber == 13).ToList();
                    userBudgetEntries?.AddRange(itsBudgetEntries);
                    BudgetCurrency itsBudgetCurrency = await BudgetCurrencyService!.GetByYearVersionCustomerArticleAsync(budgetStartYear,
                    article, customer, yearlyLock?.Name);
                    if (!itsBudgetCurrency!.PlanningComplete)
                    {
                        if (article is not null) articles.Add(article);
                    }
                }
            }
        }
    }
        protected async Task BudgetCurrencyChanged(ChangeEventArgs<string, Currency> args)
        {
            if (budgetCurrency is not null && budgetCurrency.Id != 0)
            {
                await budgetCurrency.SetModifyInfo(authenticationStateProvider!);
                await BudgetCurrencyService!.UpdateEntityAsync(budgetCurrency);
                await LoadExchangeRatesAsync();
            }
        }

        protected async Task VolumeTemplateSelected(ChangeEventArgs<int, VolumeTemplate> args)
        {
            if (!VolumeTemplateService!.SingleLoading) volumeTemplate = await VolumeTemplateService.GetByIdAsync(args.ItemData.Id);
        }
        protected async Task YearlyLockSelected(ChangeEventArgs<int, YearlyLock> args)
        {
            if (!YearlyLockService!.SingleLoading) yearlyLock = await YearlyLockService.GetByIdAsync(args.ItemData.Id);
            budgetEntries = new();
            budgetCurrency = new();
            if (!BudgetEntryService!.ListLoading && customer is not null && article is not null && yearlyLock is not null)
            {
                budgetEntries = await BudgetEntryService.GetByYearVersionCustomerArticleAsync(budgetStartYear, article, customer, yearlyLock.Name, authenticationStateProvider);
                budgetCurrency = await BudgetCurrencyService!.GetByYearVersionCustomerArticleAsync(budgetStartYear, article, customer, yearlyLock.Name);
                await PopulateFC1ActualDataAsync();
                await PopulateFC2ActualDataAsync();
                await PopulateFC3ActualDataAsync();
                await LoadCurrencyInfoAsync();
            }
        }
        protected async Task BudgetYearDecrement()
        {
            budgetStartYear--;
            budgetEntries = new();
            yearlyLocks = await YearlyLockService!.GetByYearAsync(budgetStartYear);
            yearlyLock = new();
            budgetCurrency = new();
            if (customer is not null && article is not null)
            {
                await PopulateFC1ActualDataAsync();
                await PopulateFC2ActualDataAsync();
                await PopulateFC3ActualDataAsync();
            }
        }
        protected async Task ActualDataYearDecrement()
        {
            actualDataYear--;
            actualDataEntries = new();
            yearlyLocks = await YearlyLockService!.GetByYearAsync(budgetStartYear);
            await PopulateActualDataAsync();
        }
        protected async Task BudgetYearIncrement()
        {
            budgetStartYear++;
            budgetEntries = new();
            yearlyLocks = await YearlyLockService!.GetByYearAsync(budgetStartYear);
            yearlyLock = new();
            budgetCurrency = new();
            if (customer is not null && article is not null)
            {
                await PopulateFC1ActualDataAsync();
                await PopulateFC2ActualDataAsync();
                await PopulateFC3ActualDataAsync();
                await LoadExchangeRatesAsync();
            }
        }
        protected async Task ActualDataYearIncrement()
        {
            actualDataYear++;
            actualDataEntries = new();
            await PopulateActualDataAsync();
        }
        protected async Task PopulateActualDataAsync()
        {
            actualDataEntries = new();
            if ((customer is not null) && (article is not null) && (customer.BpCode != "") && (article.ItemNo != "") && (customer.BpCode is not null) && (article.ItemNo is not null))
            {
                for (int actualDataGenerator = 1; actualDataGenerator <= 12; actualDataGenerator++)
                {
                    ActualDataEntry actualDataEntry = new ActualDataEntry(actualDataYear, actualDataGenerator);
                    var itsTransactions = await TransactionService!.GetForActualDataAsync(customer.BpCode, article.ItemNo, actualDataEntry.MonthNr, actualDataEntry.YearName);
                    budgetCurrency = await BudgetCurrencyService!.GetByYearVersionCustomerArticleAsync(budgetStartYear, article, customer, yearlyLock?.Name);
                    actualDataEntry.CurrencyDc = budgetCurrency?.DocumentCurrencyCode;
                    actualDataEntry.CurrencyLc = budgetCurrency?.LocalCurrencyCode;
                    var itsExchangeRates = await ExchangeRateService!.GetByCurrencyPairYearAsync(actualDataEntry!.CurrencyDc, actualDataEntry!.CurrencyLc, actualDataEntry.YearName);
                    if (itsTransactions is not null)
                    {
                        actualDataEntry.Quantity = itsTransactions.Sum(t => t.Quantity);
                        actualDataEntry.SalesLC = itsTransactions.Sum(t => t.SalesLc + t.SurchargeLc);
                        actualDataEntry.SalesDC = itsTransactions.Sum(t => t.SalesDc + t.SurchargeDc);
                        if (actualDataEntry.Quantity != 0)
                        {
                            actualDataEntry.SurchargeLC = itsTransactions.Sum(t => t.SurchargeLc) / actualDataEntry.Quantity;
                            actualDataEntry.SurchargeDC = itsTransactions.Sum(t => t.SurchargeDc) / actualDataEntry.Quantity;
                        }
                        else
                        {
                            actualDataEntry.SurchargeLC = 0;
                            actualDataEntry.SurchargeDC = 0;
                        }
                        if(itsExchangeRates is not null && actualDataEntry.CurrencyDc =="EUR" && actualDataEntry.CurrencyLc != "EUR")
                        {
                            actualDataEntry.PriceEur = (itsExchangeRates!.ExchangeValue) * (actualDataEntry.NetPriceLC);
                            actualDataEntry.SalesEur = itsExchangeRates!.ExchangeValue * actualDataEntry.SalesLC;
                        }
                        else
                        {
                            actualDataEntry.PriceEur = itsExchangeRates!.ExchangeValue;
                            actualDataEntry.SalesEur = itsExchangeRates!.ExchangeValue;
                        }
                    }
                    actualDataEntries.Add(actualDataEntry);
                }
            }
        }
        protected async Task PopulateFC1ActualDataAsync()
        {
            fC1ActualDataEntries = new();
            if ((customer is not null) && (article is not null) && (customer.BpCode != "") && (article.ItemNo != "") && (customer.BpCode is not null) && (article.ItemNo is not null))
            {
                for (int actualDataGenerator = 1; actualDataGenerator <= 3; actualDataGenerator++)
                {
                    ActualDataEntry fC1ActualDataEntry = new ActualDataEntry(budgetStartYear - 1, actualDataGenerator);
                    budgetCurrency = await BudgetCurrencyService!.GetByYearVersionCustomerArticleAsync(budgetStartYear, article, customer, yearlyLock?.Name);
                    fC1ActualDataEntry.CurrencyDc = budgetCurrency?.DocumentCurrencyCode;
                    fC1ActualDataEntry.CurrencyLc = budgetCurrency?.LocalCurrencyCode;
                    var itsExchangeRates = await ExchangeRateService!.GetByCurrencyPairYearAsync(fC1ActualDataEntry!.CurrencyDc, fC1ActualDataEntry!.CurrencyLc, fC1ActualDataEntry.YearName);
                    var itsTransactions = await TransactionService!.GetForActualDataAsync(customer.BpCode, article.ItemNo, fC1ActualDataEntry.MonthNr, fC1ActualDataEntry.YearName);
                    if (itsTransactions is not null)
                    {
                        fC1ActualDataEntry.Quantity = itsTransactions.Sum(t => t.Quantity);
                        fC1ActualDataEntry.SalesLC = itsTransactions.Sum(t => t.SalesLc + t.SurchargeLc);
                        fC1ActualDataEntry.SalesDC = itsTransactions.Sum(t => t.SalesDc + t.SurchargeDc);
                        fC1ActualDataEntry.SurchargeLC = itsTransactions.Sum(t => t.SurchargeLc) / fC1ActualDataEntry.Quantity;
                        fC1ActualDataEntry.SurchargeDC = itsTransactions.Sum(t => t.SurchargeDc) / fC1ActualDataEntry.Quantity;
                        if (fC1ActualDataEntry.Quantity != 0)
                        {
                            fC1ActualDataEntry.SurchargeLC = itsTransactions.Sum(t => t.SurchargeLc) / fC1ActualDataEntry.Quantity;
                            fC1ActualDataEntry.SurchargeDC = itsTransactions.Sum(t => t.SurchargeDc) / fC1ActualDataEntry.Quantity;
                        }
                        else
                        {
                            fC1ActualDataEntry.SurchargeLC = 0;
                            fC1ActualDataEntry.SurchargeDC = 0;
                        }
                    }
                    fC1ActualDataEntries.Add(fC1ActualDataEntry);
                }
            }
        }
        protected async Task PopulateFC2ActualDataAsync()
        {
            fC2ActualDataEntries = new();
            if ((customer is not null) && (article is not null) && (customer.BpCode != "") && (article.ItemNo != "") && (customer.BpCode is not null) && (article.ItemNo is not null))
            {
                for (int actualDataGenerator = 1; actualDataGenerator <= 6; actualDataGenerator++)
                {
                    ActualDataEntry fC2ActualDataEntry = new ActualDataEntry(budgetStartYear - 1, actualDataGenerator);
                    budgetCurrency = await BudgetCurrencyService!.GetByYearVersionCustomerArticleAsync(budgetStartYear, article, customer, yearlyLock?.Name);
                    fC2ActualDataEntry.CurrencyDc = budgetCurrency?.DocumentCurrencyCode;
                    fC2ActualDataEntry.CurrencyLc = budgetCurrency?.LocalCurrencyCode;
                    var itsExchangeRates = await ExchangeRateService!.GetByCurrencyPairYearAsync(fC2ActualDataEntry!.CurrencyDc, fC2ActualDataEntry!.CurrencyLc, fC2ActualDataEntry.YearName);
                    var itsTransactions = await TransactionService!.GetForActualDataAsync(customer.BpCode, article.ItemNo, fC2ActualDataEntry.MonthNr, fC2ActualDataEntry.YearName);
                    if (itsTransactions is not null)
                    {
                        fC2ActualDataEntry.Quantity = itsTransactions.Sum(t => t.Quantity);
                        fC2ActualDataEntry.SalesLC = itsTransactions.Sum(t => t.SalesLc + t.SurchargeLc);
                        fC2ActualDataEntry.SalesDC = itsTransactions.Sum(t => t.SalesDc + t.SurchargeDc);
                        fC2ActualDataEntry.SurchargeLC = itsTransactions.Sum(t => t.SurchargeLc) / fC2ActualDataEntry.Quantity;
                        fC2ActualDataEntry.SurchargeDC = itsTransactions.Sum(t => t.SurchargeDc) / fC2ActualDataEntry.Quantity;
                        if (fC2ActualDataEntry.Quantity != 0)
                        {
                            fC2ActualDataEntry.SurchargeLC = itsTransactions.Sum(t => t.SurchargeLc) / fC2ActualDataEntry.Quantity;
                            fC2ActualDataEntry.SurchargeDC = itsTransactions.Sum(t => t.SurchargeDc) / fC2ActualDataEntry.Quantity;
                        }
                        else
                        {
                            fC2ActualDataEntry.SurchargeLC = 0;
                            fC2ActualDataEntry.SurchargeDC = 0;
                        }
                        if(itsExchangeRates is not null && fC2ActualDataEntry.CurrencyDc =="EUR" && fC2ActualDataEntry.CurrencyLc != "EUR")
                        {
                            fC2ActualDataEntry.PriceEur = (itsExchangeRates!.ExchangeValue) * (fC2ActualDataEntry.NetPriceLC);
                            fC2ActualDataEntry.SalesEur = itsExchangeRates!.ExchangeValue * fC2ActualDataEntry.SalesLC;
                        }
                        else
                        {
                            fC2ActualDataEntry.PriceEur = itsExchangeRates!.ExchangeValue;
                            fC2ActualDataEntry.SalesEur = itsExchangeRates!.ExchangeValue;
                        }
                    }
                    fC2ActualDataEntries.Add(fC2ActualDataEntry);
                }
            }
        }
        protected async Task PopulateFC3ActualDataAsync()
        {
            fC3ActualDataEntries = new();
            if ((customer is not null) && (article is not null) && (customer.BpCode != "") && (article.ItemNo != "") && (customer.BpCode is not null) && (article.ItemNo is not null))
            {
                for (int actualDataGenerator = 1; actualDataGenerator <= 9; actualDataGenerator++)
                {
                    ActualDataEntry fC3ActualDataEntry = new ActualDataEntry(budgetStartYear - 1, actualDataGenerator);
                    budgetCurrency = await BudgetCurrencyService!.GetByYearVersionCustomerArticleAsync(budgetStartYear, article, customer, yearlyLock?.Name);
                    fC3ActualDataEntry.CurrencyDc = budgetCurrency?.DocumentCurrencyCode;
                    fC3ActualDataEntry.CurrencyLc = budgetCurrency?.LocalCurrencyCode;
                    var itsExchangeRates = await ExchangeRateService!.GetByCurrencyPairYearAsync(fC3ActualDataEntry!.CurrencyDc, fC3ActualDataEntry!.CurrencyLc, fC3ActualDataEntry.YearName);
                    var itsTransactions = await TransactionService!.GetForActualDataAsync(customer.BpCode, article.ItemNo, fC3ActualDataEntry.MonthNr, fC3ActualDataEntry.YearName);
                    if (itsTransactions is not null)
                    {
                        fC3ActualDataEntry.Quantity = itsTransactions.Sum(t => t.Quantity);
                        fC3ActualDataEntry.SalesLC = itsTransactions.Sum(t => t.SalesLc + t.SurchargeLc);
                        fC3ActualDataEntry.SalesDC = itsTransactions.Sum(t => t.SalesDc + t.SurchargeDc);
                        fC3ActualDataEntry.SurchargeLC = itsTransactions.Sum(t => t.SurchargeLc) / fC3ActualDataEntry.Quantity;
                        fC3ActualDataEntry.SurchargeDC = itsTransactions.Sum(t => t.SurchargeDc) / fC3ActualDataEntry.Quantity;
                        if (fC3ActualDataEntry.Quantity != 0)
                        {
                            fC3ActualDataEntry.SurchargeLC = itsTransactions.Sum(t => t.SurchargeLc) / fC3ActualDataEntry.Quantity;
                            fC3ActualDataEntry.SurchargeDC = itsTransactions.Sum(t => t.SurchargeDc) / fC3ActualDataEntry.Quantity;
                        }
                        else
                        {
                            fC3ActualDataEntry.SurchargeLC = 0;
                            fC3ActualDataEntry.SurchargeDC = 0;
                        }
                        if(itsExchangeRates is not null && fC3ActualDataEntry.CurrencyDc =="EUR" && fC3ActualDataEntry.CurrencyLc != "EUR")
                        {
                            fC3ActualDataEntry.PriceEur = (itsExchangeRates!.ExchangeValue) * (fC3ActualDataEntry.NetPriceLC);
                            fC3ActualDataEntry.SalesEur = itsExchangeRates!.ExchangeValue * fC3ActualDataEntry.SalesLC;
                        }
                        else
                        {
                            fC3ActualDataEntry.PriceEur = itsExchangeRates!.ExchangeValue;
                            fC3ActualDataEntry.SalesEur = itsExchangeRates!.ExchangeValue;
                        }
                    }
                    fC3ActualDataEntries.Add(fC3ActualDataEntry);
                }
            }
        }
        // protected async Task CellEdit(CellEditArgs<BudgetEntry> args)
        // {
        //     // args.Data = args.Data ?? new BudgetEntry()
        //     // {
        //     //     BudgetPrice = args.RowData.BudgetPrice,
        //     //     BudgetSurcharge = args.RowData.BudgetSurcharge,
        //     //     BudgetQuantity = args.RowData.BudgetQuantity
        //     // };
        //     // await args.Data.SetModifyInfo(authenticationStateProvider!);
        //     // await BudgetEntryService!.UpdateEntityAsync(args.Data);
        // }
        // protected async Task CellSavedHandler(CellSaveArgs<BudgetEntry> args)
        // {
        //     await args.Data.SetModifyInfo(authenticationStateProvider!);
        //     await BudgetEntryService!.UpdateEntityAsync(args.Data);
        //     var index = await Grid!.GetRowIndexByPrimaryKey(args.RowData.Id);
        //     if (IsAdd)
        //     {

        //     }
        //     else
        //     {
        //         //args.RowData.BudgetQuantity = (double)args.Value; 
        //         //await Grid.UpdateCellAsync(index, args.ColumnName ="BudgetSales", Convert.ToDouble(args.RowData.BudgetQuantity) * (args.RowData.BudgetPrice + args.RowData.BudgetSurcharge));
        //         // Grid?.EndEditAsync();
        //         // Grid?.Refresh(); 
        //         // await Grid!.RefreshColumnsAsync(); 
        //         // await args.RowData.SetModifyInfo(authenticationStateProvider!);
        //         // await BudgetEntryService!.UpdateEntityAsync(args.RowData);
        //     }
        // }
        protected async Task BudgetEntryActionBeginHandler(ActionEventArgs<BudgetEntry> Args)
        {
            if (Args.RequestType.Equals(Syncfusion.Blazor.Grids.Action.Save))
            {
                if (Args.Action == "Add")
                {
                    await Args.Data.SetCreateInfo(authenticationStateProvider!);
                    await BudgetEntryService!.AddEntityAsync(Args.Data);
                }
                else
                {
                    await Args.Data.SetModifyInfo(authenticationStateProvider!);
                    await BudgetEntryService!.UpdateEntityAsync(Args.Data);
                }
            }
        }
        public async Task BatchSaveHandler(BeforeBatchSaveArgs<BudgetEntry> args)
        {
            foreach (BudgetEntry entry in args.BatchChanges.ChangedRecords)
            {
                await entry.SetModifyInfo(authenticationStateProvider!);
                await BudgetEntryService!.UpdateEntityAsync(entry);
            }
        }
        public void OnBatchCancel(BeforeBatchCancelArgs<BudgetEntry> args)
        {
            //args.BatchChanges.ChangedRecords.ToList();
        }
        protected async Task BudgetEntryActionCompleteHandler(ActionEventArgs<BudgetEntry> Args)
        {
            if (Args.RequestType.Equals(Syncfusion.Blazor.Grids.Action.Save))
            {
                if (!BudgetEntryService!.ListLoading && customer is not null && article is not null && yearlyLock is not null)
                {
                    budgetEntries = await BudgetEntryService.GetByYearVersionCustomerArticleAsync(budgetStartYear, article, customer, yearlyLock.Name, authenticationStateProvider);
                }
            }
        }
        public void BudgetDataBoundHandler1(RowDataBoundEventArgs<BudgetEntry> args)
        {
            if (args.Data.BudgetAllowed)
            {
                if (args.Data.YearNumber == budgetStartYear)
                {
                    
                }
                else
                {
                    args.Row.AddClass(new string[] { "e-inactive" });
                }
            }
            else
            {

                args.Row.AddClass(new string[] { "e-inactive" });
            }
        }
        public void BudgetDataBoundHandler2(RowDataBoundEventArgs<BudgetEntry> args)
        {
            if (args.Data.BudgetAllowed)
            {
                if (args.Data.YearNumber > budgetStartYear)
                {

                }
                else
                {
                    args.Row.AddClass(new string[] { "e-inactive" });
                }
            }
            else
            {
                args.Row.AddClass(new string[] { "e-inactive" });
            }
        }
        public void FC1DataBoundHandler(RowDataBoundEventArgs<BudgetEntry> args)
        {
            if (args.Data.FC1Allowed)
            {

            }
            else
            {
                args.Row.AddClass(new string[] { "e-inactive" });
            }
        }
        public void FC2DataBoundHandler(RowDataBoundEventArgs<BudgetEntry> args)
        {
            if (args.Data.FC2Allowed)
            {

            }
            else
            {
                args.Row.AddClass(new string[] { "e-inactive" });
            }
        }
        public void FC3DataBoundHandler(RowDataBoundEventArgs<BudgetEntry> args)
        {
            if (args.Data.FC3Allowed)
            {

            }
            else
            {
                args.Row.AddClass(new string[] { "e-inactive" });
            }
        }
        protected async Task ApplyTemplateAsync(int year)
        {
            if (budgetEntries is not null && volumeTemplate is not null)
            {
                foreach (BudgetEntry budgetEntry in budgetEntries)
                {
                    if (budgetEntry.BudgetAllowed)
                    {
                        if (budgetEntry.MonthNumber == 1) budgetEntry.VolumePercentage = volumeTemplate.Month1Percentage/100;
                        if (budgetEntry.MonthNumber == 2) budgetEntry.VolumePercentage = volumeTemplate.Month2Percentage/100;
                        if (budgetEntry.MonthNumber == 3) budgetEntry.VolumePercentage = volumeTemplate.Month3Percentage/100;
                        if (budgetEntry.MonthNumber == 4) budgetEntry.VolumePercentage = volumeTemplate.Month4Percentage/100;
                        if (budgetEntry.MonthNumber == 5) budgetEntry.VolumePercentage = volumeTemplate.Month5Percentage/100;
                        if (budgetEntry.MonthNumber == 6) budgetEntry.VolumePercentage = volumeTemplate.Month6Percentage/100;
                        if (budgetEntry.MonthNumber == 7) budgetEntry.VolumePercentage = volumeTemplate.Month7Percentage/100;
                        if (budgetEntry.MonthNumber == 8) budgetEntry.VolumePercentage = volumeTemplate.Month8Percentage/100;
                        if (budgetEntry.MonthNumber == 9) budgetEntry.VolumePercentage = volumeTemplate.Month9Percentage/100;
                        if (budgetEntry.MonthNumber == 10) budgetEntry.VolumePercentage = volumeTemplate.Month10Percentage/100;
                        if (budgetEntry.MonthNumber == 11) budgetEntry.VolumePercentage = volumeTemplate.Month11Percentage/100;
                        if (budgetEntry.MonthNumber == 12) budgetEntry.VolumePercentage = volumeTemplate.Month12Percentage/100;
                        if(year == 1)
                        {
                            if (budgetEntry.YearNumber == budgetStartYear)
                            {
                                if (defaultPrice != 0) budgetEntry.BudgetPrice = defaultPrice;
                                if (defaultSurcharge != 0) 
                                {
                                    budgetEntry.BudgetSurcharge = (defaultSurcharge * budgetEntry.BudgetPrice)/100;
                                    if (defaultPrice!= 0) budgetEntry.BudgetSurcharge = (defaultSurcharge * defaultPrice)/100;
                                }    
                                if (annualQuantity != 0) budgetEntry.BudgetQuantity = annualQuantity * budgetEntry.VolumePercentage;
                            }
                        } 
                        else if(year == 2)
                        {
                            if(budgetEntry.YearNumber == budgetStartYear + 1)
                            {
                                if (defaultPriceNextYear != 0) budgetEntry.BudgetPrice = defaultPriceNextYear;
                                if (defaultSurchargeNextYear != 0) 
                                {
                                    budgetEntry.BudgetSurcharge = (defaultSurchargeNextYear * budgetEntry.BudgetPrice)/100;
                                    if (defaultPriceNextYear!= 0) budgetEntry.BudgetSurcharge = (defaultSurchargeNextYear * defaultPriceNextYear)/100;
                                }    
                                if (annualQuantityNextYear != 0) budgetEntry.BudgetQuantity = annualQuantityNextYear * budgetEntry.VolumePercentage;
                            }
                        }
                        await budgetEntry.SetModifyInfo(authenticationStateProvider!);
                        await BudgetEntryService!.UpdateEntityAsync(budgetEntry);
                    }
                }
                if (!BudgetEntryService!.ListLoading && customer is not null && article is not null && yearlyLock is not null)
                {
                    budgetEntries = await BudgetEntryService.GetByYearVersionCustomerArticleAsync(budgetStartYear, article, customer, yearlyLock.Name, authenticationStateProvider);
                }
            }
        }
        protected async Task LoadCurrencyInfoAsync()
        {
            if (customer is not null && article is not null && customer.BpCode is not null && article.ItemNo is not null && budgetCurrency is not null && dbwEntity is not null)
            {
                var interestingCurrencies = await EntityLocalCurrencyService!.GetByEntityAsync(dbwEntity);
                if (budgetCurrency.DocumentCurrencyCode is null) budgetCurrency.DocumentCurrencyCode = interestingCurrencies.DocumentCurrencyCode;
                if (budgetCurrency.LocalCurrencyCode is null) budgetCurrency.LocalCurrencyCode = interestingCurrencies.LocalCurrencyCode;
                await budgetCurrency.SetModifyInfo(authenticationStateProvider!);
                await BudgetCurrencyService!.UpdateEntityAsync(budgetCurrency);
                await LoadExchangeRatesAsync();
            }
        }
        protected async Task LoadExchangeRatesAsync()
        {
            exchangeRates = new();
            if (budgetCurrency is not null && budgetCurrency.DocumentCurrencyCode is not null && budgetCurrency.LocalCurrencyCode is not null)
            {
                exchangeRates = new();
                for (var yearIterator = budgetStartYear - 1; yearIterator <= this.budgetStartYear + 1; yearIterator++)
                {
                    ExchangeRate xR = new();
                    if (budgetCurrency.DocumentCurrencyCode != budgetCurrency.LocalCurrencyCode)
                    {
                        xR = await ExchangeRateService!.GetByCurrencyPairYearAsync(budgetCurrency.DocumentCurrencyCode, budgetCurrency.LocalCurrencyCode, yearIterator);
                        exchangeRates.Add(xR);
                        if (yearIterator == budgetStartYear - 1) xRDL0 = xR.ExchangeValue;
                        if (yearIterator == budgetStartYear) xRDL1 = xR.ExchangeValue;
                        if (yearIterator == budgetStartYear + 1) xRDL2 = xR.ExchangeValue;
                    }
                    else
                    {
                        xRDL0 = 1;
                        xRDL1 = 1;
                        xRDL2 = 1;
                    }
                    if (budgetCurrency.DocumentCurrencyCode != "EUR" && budgetCurrency.LocalCurrencyCode != "EUR")
                    {
                        xR = await ExchangeRateService!.GetByCurrencyPairYearAsync(budgetCurrency.DocumentCurrencyCode, "EUR", yearIterator);
                        exchangeRates.Add(xR);
                        if (yearIterator == budgetStartYear - 1) xRDE0 = xR.ExchangeValue;
                        if (yearIterator == budgetStartYear) xRDE1 = xR.ExchangeValue;
                        if (yearIterator == budgetStartYear + 1) xRDE2 = xR.ExchangeValue;
                    }
                    else
                    {
                        xRDE0 = 1;
                        xRDE1 = 1;
                        xRDE2 = 1;
                    }
                }
            }
        }
        public string Year1QuantityTotal(AggregateTemplateContext val)
        {
            if (budgetEntries is not null)
            {
                var data = budgetEntries.Where(e => e.BudgetAllowed = true && e.YearNumber == budgetStartYear);
                return Queryable.Sum(data.Select(x => x.BudgetQuantity).AsQueryable()).ToString("N0");
            }
            return "0";
        }
        public string Year1SalesDCTotal(AggregateTemplateContext val)
        {
            if (budgetEntries is not null)
            {
                var data = budgetEntries.Where(e => e.BudgetAllowed = true && e.YearNumber == budgetStartYear);
                return Queryable.Sum(data.Select(x => x.BudgetSales).AsQueryable()).ToString("N0");
            }
            return "0";
        }
        public async Task TogglePlanComplete()
        {
            budgetCurrency!.PlanningComplete = !budgetCurrency.PlanningComplete;
            await budgetCurrency.SetModifyInfo(authenticationStateProvider!);
            await BudgetCurrencyService!.UpdateEntityAsync(budgetCurrency);
            if (budgetCurrency!.PlanningComplete)
            {
                countIncompleteAeticles --;
                countCompleteArticles ++;
            }
            else
            {
                countCompleteArticles --;
                countIncompleteAeticles ++;
            }
        }
        public string Year2QuantityTotal(AggregateTemplateContext val)
        {
            if (budgetEntries is not null)
            {
                var data = budgetEntries.Where(e => e.BudgetAllowed = true && e.YearNumber > budgetStartYear);
                return Queryable.Sum(data.Select(x => x.BudgetQuantity).AsQueryable()).ToString("N0");
            }
            return "0";
        }
        public string Year2SalesDCTotal(AggregateTemplateContext val)
        {
            if (budgetEntries is not null)
            {
                var data = budgetEntries.Where(e => e.BudgetAllowed = true && e.YearNumber > budgetStartYear);
                return Queryable.Sum(data.Select(x => x.BudgetSales).AsQueryable()).ToString("N0");
            }
            return "0";
        }

        public string FC1SalesDCTotal(AggregateTemplateContext val)
        {
            double resultNr = 0;
            if (budgetEntries is not null)
            {
                var data = budgetEntries.Where(e => e.BudgetAllowed = true && e.YearNumber < budgetStartYear);
                resultNr = Queryable.Sum(data.Select(x => x.FC1Sales).AsQueryable());
            }
            if (fC1ActualDataEntries is not null)
            {
                resultNr += Queryable.Sum(fC1ActualDataEntries.Select(x => x.SalesDC).AsQueryable());
            }
            return resultNr.ToString("N0");
        }
        public string FC2SalesDCTotal(AggregateTemplateContext val)
        {
            double resultNr = 0;
            if (budgetEntries is not null)
            {
                var data = budgetEntries.Where(e => e.BudgetAllowed = true && e.YearNumber < budgetStartYear);
                resultNr = Queryable.Sum(data.Select(x => x.FC2Sales).AsQueryable());
            }
            if (fC2ActualDataEntries is not null)
            {
                resultNr += Queryable.Sum(fC2ActualDataEntries.Select(x => x.SalesDC).AsQueryable());
            }
            return resultNr.ToString("N0");
        }
        public string FC3SalesDCTotal(AggregateTemplateContext val)
        {
            double resultNr = 0;
            if (budgetEntries is not null)
            {
                var data = budgetEntries.Where(e => e.BudgetAllowed = true && e.YearNumber < budgetStartYear);
                resultNr = Queryable.Sum(data.Select(x => x.FC3Sales).AsQueryable());
            }
            if (fC3ActualDataEntries is not null)
            {
                resultNr += Queryable.Sum(fC3ActualDataEntries.Select(x => x.SalesDC).AsQueryable());
            }
            return resultNr.ToString("N0");
        }
        public string FC1QuantityTotal(AggregateTemplateContext val)
        {
            double resultNr = 0;
            if (budgetEntries is not null)
            {
                var data = budgetEntries.Where(e => e.BudgetAllowed = true && e.YearNumber < budgetStartYear);
                resultNr = Queryable.Sum(data.Select(x => x.FC1Quantity).AsQueryable());
            }
            if (fC1ActualDataEntries is not null)
            {
                resultNr += Queryable.Sum(fC1ActualDataEntries.Select(x => x.Quantity).AsQueryable());
            }
            return resultNr.ToString("N0");
        }
        public string FC2QuantityTotal(AggregateTemplateContext val)
        {
            double resultNr = 0;
            if (budgetEntries is not null)
            {
                var data = budgetEntries.Where(e => e.BudgetAllowed = true && e.YearNumber < budgetStartYear);
                resultNr = Queryable.Sum(data.Select(x => x.FC2Quantity).AsQueryable());
            }
            if (fC2ActualDataEntries is not null)
            {
                resultNr += Queryable.Sum(fC2ActualDataEntries.Select(x => x.Quantity).AsQueryable());
            }
            return resultNr.ToString("N0");
        }
        public string FC3QuantityTotal(AggregateTemplateContext val)
        {
            double resultNr = 0;
            if (budgetEntries is not null)
            {
                var data = budgetEntries.Where(e => e.BudgetAllowed = true && e.YearNumber < budgetStartYear);
                resultNr = Queryable.Sum(data.Select(x => x.FC3Quantity).AsQueryable());
            }
            if (fC3ActualDataEntries is not null)
            {
                resultNr += Queryable.Sum(fC3ActualDataEntries.Select(x => x.Quantity).AsQueryable());
            }
            return resultNr.ToString("N0");
        }
        protected async Task ApplyTemplateForFC1Async()
        {
            if (budgetEntries is not null && volumeTemplate is not null)
            {
                foreach (BudgetEntry budgetEntry in budgetEntries)
                {
                    if (budgetEntry.FC1Allowed)
                    {
                        if (budgetEntry.MonthNumber == 1) budgetEntry.VolumePercentage = volumeTemplate.Month1Percentage/100;
                        if (budgetEntry.MonthNumber == 2) budgetEntry.VolumePercentage = volumeTemplate.Month2Percentage/100;
                        if (budgetEntry.MonthNumber == 3) budgetEntry.VolumePercentage = volumeTemplate.Month3Percentage/100;
                        if (budgetEntry.MonthNumber == 4) budgetEntry.VolumePercentage = volumeTemplate.Month4Percentage/100;
                        if (budgetEntry.MonthNumber == 5) budgetEntry.VolumePercentage = volumeTemplate.Month5Percentage/100;
                        if (budgetEntry.MonthNumber == 6) budgetEntry.VolumePercentage = volumeTemplate.Month6Percentage/100;
                        if (budgetEntry.MonthNumber == 7) budgetEntry.VolumePercentage = volumeTemplate.Month7Percentage/100;
                        if (budgetEntry.MonthNumber == 8) budgetEntry.VolumePercentage = volumeTemplate.Month8Percentage/100;
                        if (budgetEntry.MonthNumber == 9) budgetEntry.VolumePercentage = volumeTemplate.Month9Percentage/100;
                        if (budgetEntry.MonthNumber == 10) budgetEntry.VolumePercentage = volumeTemplate.Month10Percentage/100;
                        if (budgetEntry.MonthNumber == 11) budgetEntry.VolumePercentage = volumeTemplate.Month11Percentage/100;
                        if (budgetEntry.MonthNumber == 12) budgetEntry.VolumePercentage = volumeTemplate.Month12Percentage/100;
                        if (defaultPriceForecast != 0) budgetEntry.FC1Price = defaultPriceForecast;
                        if (defaultSurchargeForecast != 0) 
                        {
                            budgetEntry.FC1Surcharge = (defaultSurchargeForecast * budgetEntry.FC1Price)/100;
                            if (defaultPriceForecast!= 0) budgetEntry.FC1Surcharge = (defaultSurchargeForecast * defaultPriceForecast)/100;
                        }    
                        if (annualQuantityForecast != 0) budgetEntry.FC1Quantity = annualQuantityForecast * budgetEntry.VolumePercentage;
                        // else if(year == 2)
                        // {
                        //     if(budgetEntry.YearNumber == budgetStartYear + 1)
                        //     {
                        //         if (defaultPriceNextYear != 0) budgetEntry.FC2Price = defaultPriceNextYear;
                        //         if (defaultSurchargeNextYear != 0) 
                        //         {
                        //             budgetEntry.BudgetSurcharge = (defaultSurchargeNextYear * budgetEntry.BudgetPrice)/100;
                        //             if (defaultPriceNextYear!= 0) budgetEntry.BudgetSurcharge = (defaultSurchargeNextYear * defaultPriceNextYear)/100;
                        //         }    
                        //         if (annualQuantityForecastNextYear != 0) budgetEntry.BudgetQuantity = annualQuantityForecastNextYear * budgetEntry.VolumePercentage;
                        //     }
                        // }
                        await budgetEntry.SetModifyInfo(authenticationStateProvider!);
                        await BudgetEntryService!.UpdateEntityAsync(budgetEntry);
                    }
                }
                if (!BudgetEntryService!.ListLoading && customer is not null && article is not null && yearlyLock is not null)
                {
                    budgetEntries = await BudgetEntryService.GetByYearVersionCustomerArticleAsync(budgetStartYear, article, customer, yearlyLock.Name, authenticationStateProvider);
                }
            }
        }
        protected async Task ApplyTemplateForFC2Async()
        {
            if (budgetEntries is not null && volumeTemplate is not null)
            {
                foreach (BudgetEntry budgetEntry in budgetEntries)
                {
                    if (budgetEntry.FC2Allowed)
                    {
                        if (budgetEntry.MonthNumber == 1) budgetEntry.VolumePercentage = volumeTemplate.Month1Percentage/100;
                        if (budgetEntry.MonthNumber == 2) budgetEntry.VolumePercentage = volumeTemplate.Month2Percentage/100;
                        if (budgetEntry.MonthNumber == 3) budgetEntry.VolumePercentage = volumeTemplate.Month3Percentage/100;
                        if (budgetEntry.MonthNumber == 4) budgetEntry.VolumePercentage = volumeTemplate.Month4Percentage/100;
                        if (budgetEntry.MonthNumber == 5) budgetEntry.VolumePercentage = volumeTemplate.Month5Percentage/100;
                        if (budgetEntry.MonthNumber == 6) budgetEntry.VolumePercentage = volumeTemplate.Month6Percentage/100;
                        if (budgetEntry.MonthNumber == 7) budgetEntry.VolumePercentage = volumeTemplate.Month7Percentage/100;
                        if (budgetEntry.MonthNumber == 8) budgetEntry.VolumePercentage = volumeTemplate.Month8Percentage/100;
                        if (budgetEntry.MonthNumber == 9) budgetEntry.VolumePercentage = volumeTemplate.Month9Percentage/100;
                        if (budgetEntry.MonthNumber == 10) budgetEntry.VolumePercentage = volumeTemplate.Month10Percentage/100;
                        if (budgetEntry.MonthNumber == 11) budgetEntry.VolumePercentage = volumeTemplate.Month11Percentage/100;
                        if (budgetEntry.MonthNumber == 12) budgetEntry.VolumePercentage = volumeTemplate.Month12Percentage/100;
                        if (defaultPrice2Forecast != 0) budgetEntry.FC2Price = defaultPrice2Forecast;
                        if (defaultSurcharge2Forecast != 0) 
                        {
                            budgetEntry.FC2Surcharge = (defaultSurcharge2Forecast * budgetEntry.FC2Price)/100;
                            if (defaultPrice2Forecast!= 0) budgetEntry.FC2Surcharge = (defaultSurcharge2Forecast * defaultPrice2Forecast)/100;
                        }    
                        if (annualQuantity2Forecast != 0) budgetEntry.FC2Quantity = annualQuantity2Forecast * budgetEntry.VolumePercentage;
                        await budgetEntry.SetModifyInfo(authenticationStateProvider!);
                        await BudgetEntryService!.UpdateEntityAsync(budgetEntry);
                    }
                }
                if (!BudgetEntryService!.ListLoading && customer is not null && article is not null && yearlyLock is not null)
                {
                    budgetEntries = await BudgetEntryService.GetByYearVersionCustomerArticleAsync(budgetStartYear, article, customer, yearlyLock.Name, authenticationStateProvider);
                }
            }
        }
        protected async Task ApplyTemplateForFC3Async()
        {
            if (budgetEntries is not null && volumeTemplate is not null)
            {
                foreach (BudgetEntry budgetEntry in budgetEntries)
                {
                    if (budgetEntry.FC3Allowed)
                    {
                        if (budgetEntry.MonthNumber == 1) budgetEntry.VolumePercentage = volumeTemplate.Month1Percentage/100;
                        if (budgetEntry.MonthNumber == 2) budgetEntry.VolumePercentage = volumeTemplate.Month2Percentage/100;
                        if (budgetEntry.MonthNumber == 3) budgetEntry.VolumePercentage = volumeTemplate.Month3Percentage/100;
                        if (budgetEntry.MonthNumber == 4) budgetEntry.VolumePercentage = volumeTemplate.Month4Percentage/100;
                        if (budgetEntry.MonthNumber == 5) budgetEntry.VolumePercentage = volumeTemplate.Month5Percentage/100;
                        if (budgetEntry.MonthNumber == 6) budgetEntry.VolumePercentage = volumeTemplate.Month6Percentage/100;
                        if (budgetEntry.MonthNumber == 7) budgetEntry.VolumePercentage = volumeTemplate.Month7Percentage/100;
                        if (budgetEntry.MonthNumber == 8) budgetEntry.VolumePercentage = volumeTemplate.Month8Percentage/100;
                        if (budgetEntry.MonthNumber == 9) budgetEntry.VolumePercentage = volumeTemplate.Month9Percentage/100;
                        if (budgetEntry.MonthNumber == 10) budgetEntry.VolumePercentage = volumeTemplate.Month10Percentage/100;
                        if (budgetEntry.MonthNumber == 11) budgetEntry.VolumePercentage = volumeTemplate.Month11Percentage/100;
                        if (budgetEntry.MonthNumber == 12) budgetEntry.VolumePercentage = volumeTemplate.Month12Percentage/100;
                        if (defaultPrice3Forecast != 0) budgetEntry.FC3Price = defaultPrice3Forecast;
                        if (defaultSurcharge3Forecast != 0) 
                        {
                            budgetEntry.FC3Surcharge = (defaultSurcharge3Forecast * budgetEntry.FC3Price)/100;
                            if (defaultPrice3Forecast!= 0) budgetEntry.FC3Surcharge = (defaultSurcharge3Forecast * defaultPrice3Forecast)/100;
                        }    
                        if (annualQuantity3Forecast != 0) budgetEntry.FC3Quantity = annualQuantity3Forecast * budgetEntry.VolumePercentage;
                        await budgetEntry.SetModifyInfo(authenticationStateProvider!);
                        await BudgetEntryService!.UpdateEntityAsync(budgetEntry);
                    }
                }
                if (!BudgetEntryService!.ListLoading && customer is not null && article is not null && yearlyLock is not null)
                {
                    budgetEntries = await BudgetEntryService.GetByYearVersionCustomerArticleAsync(budgetStartYear, article, customer, yearlyLock.Name, authenticationStateProvider);
                }
            }
        }
    }
}