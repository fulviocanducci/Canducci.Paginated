# Canducci Paginated

**Note:** based on repository https://github.com/TroyGoode/PagedList

### Canducci Paginated 

[![Version](https://img.shields.io/nuget/v/Canducci.Pagination.svg?style=plastic&label=version)](https://www.nuget.org/packages/Canducci.Pagination/)
[![NuGet](https://img.shields.io/nuget/dt/Canducci.Pagination.svg)](https://www.nuget.org/packages/Canducci.Pagination/)

#### Canducci Paginated Mvc

[![Version](https://img.shields.io/nuget/v/Canducci.Pagination.Mvc.svg?style=plastic&label=version)](https://www.nuget.org/packages/Canducci.Pagination.Mvc/)
[![NuGet](https://img.shields.io/nuget/dt/Canducci.Pagination.Mvc.svg)](https://www.nuget.org/packages/Canducci.Pagination.Mvc/)

#### Build Test

[![Build Status](https://travis-ci.org/fulviocanducci/Canducci.Paginated.svg?branch=master)](https://travis-ci.org/fulviocanducci/Canducci.Paginated)

## Installation

- ***Canducci.Pagination***

```csharp
PM> Install-Package Canducci.Pagination
```

- ***Canducci.Pagination.Mvc***

```csharp
PM> Install-Package Canducci.Pagination.Mvc
```

**Note:** 
Installing the package `Canducci.Pagination.Mvc` already integrates the package `Canducci.Pagination`.

## How to use

##### Examples

---

- ***IQueryable***

#### Entity Framework

 ```csharp
static void TestIQueryablePaginated(int current, int total = 5)
{
	using (DatabaseContext db = new DatabaseContext())
	{
		Paginated<People> listOfQueryable0 = 
			db.People
				.OrderBy(x => x.Name)
				.ToPaginated(current, total);
	}
}
```

- ***IEnumerable***

#### StaticPaginated

```csharp
public class People
{
	public int Id { get; set; }
	public string Name { get; set; }
}

public class PeopleList: List<People>
{
	public PeopleList()
	{
		Add(new People { Id = 1, Name = "Test 1" });
		Add(new People { Id = 2, Name = "Test 2" });
		Add(new People { Id = 3, Name = "Test 3" });
		Add(new People { Id = 4, Name = "Test 4" });
		Add(new People { Id = 5, Name = "Test 5" });
		Add(new People { Id = 6, Name = "Test 6" });
		Add(new People { Id = 7, Name = "Test 7" });
		Add(new People { Id = 8, Name = "Test 8" });
		Add(new People { Id = 9, Name = "Test 9" });
		Add(new People { Id = 10, Name = "Test 10" });
	}
}
```

```csharp
static void TestIEnumerableStaticPaginated(int current, int total = 5)
{
	PeopleList listOfAllPeople = new PeopleList();
	int countOfPeople = listOfAllPeople.Count;

	IEnumerable<People> listOfPeople0 = listOfAllPeople
		.OrderBy(x => x.Id)
		.Skip((current - 1) * total)
		.Take(total)
		.ToArray();

	StaticPaginated<People> paginated0 = 
		new StaticPaginated<People>(
			listOfPeople0, 
			current, 
			total, 
			countOfPeople); 
}
```

# Mvc - Entity Framework with HtmlHelpers e TagHelpers

- ***Controller***

```csharp
using Canducci.Pagination;

public class HomeController : Controller
{
	public DatabaseContext Database { get; }

	public HomeController(DatabaseContext database)
	{
		Database = database;
	}

	public IActionResult Index(int? current)
	{
		var result = Database.People.OrderBy(x => x.Id).ToPaginated(current ?? 1, 3);
		return View(result);
	}
}
```

- ***View - Configuration (_ViewImports.cshtml)***

Open the file `_ViewImports.cshtml` and add this line  `@addTagHelper *, Canducci.Pagination.Mvc` and in the same file add `@using Canducci.Pagination.Mvc`, resume:

```csharp
@using Canducci.Pagination.Mvc
@addTagHelper *, Microsoft.AspNetCore.Mvc.TagHelpers
@addTagHelper *, Canducci.Pagination.Mvc
```
- ***View - Index.cshtml***
```csharp
@model Canducci.Pagination.Paginated<People>
@{
    ViewData["Title"] = "Home Page";
    var options = new PaginatedOptions { NextLabel = "Próximo", PreviousLabel = "Anterior", FirstLabel = "Primeiro", LastLabel = "Último" };
}

<br /><br /><br />

@foreach (var item in Model)
{
    <div>@item.Id - @item.Name</div>
}
<div>
    @Html.Pagination(Model, current => Url.Action("Index", new { current }), PaginatedStyle.PreviousNext, options)
</div>
<div>
    @Html.Pagination(Model, current => Url.Action("Index", new { current }), PaginatedStyle.FirstPreviousNextLast, options)
</div>
<div>
    @Html.Pagination(Model, current => Url.Action("Index", new { current }), PaginatedStyle.Numbers, options)
</div>
<div>
    @Html.Pagination(Model, current => Url.Action("Index", new { current}), PaginatedStyle.NumbersWithPreviousNext, options)
</div>
<div>
    @Html.Pagination(Model, current => Url.Action("Index", new { current }), PaginatedStyle.NumbersWithFirstPreviousNextLast, options)
</div>

<h3>TagHelper</h3>

<div>
    <pagination pagination-asp-action="Index"
                pagination-asp-controller="Home"
                pagination-style="PreviousNext"
                pagination-css-class-ul="pagination"
                pagination-paginated="Model"
                pagination-label-next="Próximo"
                pagination-label-previous="Anterior"
                pagination-css-class-anchor="page-link"
                pagination-css-class-li="page-item"
                pagination-css-class-li-disabled="disabled">
    </pagination>
</div>
<div>
    <pagination pagination-asp-action="Index"
                pagination-asp-controller="Home"
                pagination-style="FirstPreviousNextLast"
                pagination-css-class-ul="pagination"
                pagination-paginated="Model"
                pagination-label-next="Próximo"
                pagination-label-previous="Anterior"
                pagination-label-first="Primeiro"
                pagination-label-last="Último"
                pagination-css-class-anchor="page-link"
                pagination-css-class-li="page-item"
                pagination-css-class-li-disabled="disabled">
    </pagination>
</div>

<div>
    <pagination pagination-asp-action="Index"
                pagination-asp-controller="Home"
                pagination-style="Numbers"
                pagination-css-class-li-active="active"
                pagination-css-class-ul="pagination"
                pagination-paginated="Model"
                pagination-label-next="Próximo"
                pagination-label-previous="Anterior"
                pagination-label-first="Primeiro"
                pagination-label-last="Último"
                pagination-css-class-anchor="page-link"
                pagination-css-class-li="page-item"
                pagination-css-class-li-disabled="disabled">
    </pagination>
</div>

<div>
    <pagination pagination-asp-action="Index"
                pagination-asp-controller="Home"
                pagination-style="NumbersWithPreviousNext"
                pagination-css-class-li-active="active"
                pagination-css-class-ul="pagination"
                pagination-paginated="Model"
                pagination-label-next="Próximo"
                pagination-label-previous="Anterior"
                pagination-label-first="Primeiro"
                pagination-label-last="Último"
                pagination-css-class-anchor="page-link"
                pagination-css-class-li="page-item"
                pagination-css-class-li-disabled="disabled">
    </pagination>
</div>

<div>
    <pagination pagination-asp-action="Index"
                pagination-asp-controller="Home"
                pagination-style="NumbersWithFirstPreviousNextLast"
                pagination-css-class-li-active="active"
                pagination-css-class-ul="pagination"
                pagination-paginated="Model"
                pagination-label-next="Próximo"
                pagination-label-previous="Anterior"
                pagination-label-first="Primeiro"
                pagination-label-last="Último"
                pagination-css-class-anchor="page-link"
                pagination-css-class-li="page-item"
                pagination-css-class-li-disabled="disabled">
    </pagination>
</div>

<div>
    <pagination pagination-asp-action="Index"
                pagination-asp-controller="Home"
                pagination-style="NumbersWithFirstPreviousNextLast"                
                pagination-paginated="Model"
                pagination-label-next="Próximo"
                pagination-label-previous="Anterior">
    </pagination>
</div>
```

---

# Razor Pages

- ***PageModel Peoples***

```csharp
using System.Linq;
using Canducci.Pagination;
using Microsoft.AspNetCore.Mvc.RazorPages;
namespace Canducci.WebAppRazorPages.Test.Pages
{
    public class PeoplesModel : PageModel
    {
        private readonly DatabaseContext Context;
        public PeoplesModel(DatabaseContext context)
        {
            Context = context;            
        }

        public Paginated<People> Items { get; private set; }
        
        public void OnGet(int? current)
        {            
            Items = Context.People
                .OrderBy(x => x.Name)
                .OrderBy(x => x.Id)
                .ToPaginated(current ?? 1, 4);
        }
    }
}
```

- ***View Peoples***

```csharp
@page "page-{current=1}"
@model PeoplesModel
<br />
<table class="table table-striped">
    <thead>
        <tr>
            <th style="width:5%">Id</th>
            <th style="width:95%">Name</th>
        </tr>
    </thead>
    <tbody>
        @foreach (var item in Model.Items)
        {
        <tr>
            <td>@item.Id</td>
            <td>@item.Name</td>
        </tr>
        }
    </tbody>
    <tfoot>
        <tr>
            <td colspan="2" class="text-center">
                <pagination pagination-asp-page="Peoples"
                            pagination-asp-page-handler="page-{current=1}"
                            pagination-style="NumbersWithFirstPreviousNextLast"
                            pagination-css-class-li-active="active"
                            pagination-css-class-ul="pagination"
                            pagination-paginated="Model.Items"
                            pagination-label-next="Próximo"
                            pagination-label-previous="Anterior"
                            pagination-label-first="Primeiro"
                            pagination-label-last="Último"
                            pagination-css-class-anchor="page-link"
                            pagination-css-class-li="page-item"
                            pagination-css-class-li-disabled="disabled">
                </pagination>
            </td>            
        </tr>
        <tr>
            <td colspan="2" class="text-center">
                @Html.Pagination(Model.Items, current => Url.Page("Peoples", new { current }), PaginatedStyle.NumbersWithFirstPreviousNextLast, new PaginatedOptions { NextLabel = "Próximo", PreviousLabel = "Anterior", FirstLabel = "Primeiro", LastLabel = "Último" })
            </td>
        </tr>
    </tfoot>
</table>
```
[![Pagination-Example](https://8yi72a.by3301.livefilestore.com/y4pvOyEdXcboy4Gp0usXkb_9TGgEJcwcPSr0yg7IvF_Qnhqe_dcZF1swtL4Rfff3ZWo1ESxkg6SfwTO0urqaw3lOpIDtfoTKh6_VVPOsaRAYfRzy6lwQ1mAmBU57EMdyjZTrb7hiBi8wWPY2bDxREn9s6zFoDGY3yvr3tsD1HbERn2TQ5loOYaUkEX2t_1yB34kbBMnyp30zZkcktoahCKDFw/savebootstrap.png)](https://8yi72a.by3301.livefilestore.com/y4pvOyEdXcboy4Gp0usXkb_9TGgEJcwcPSr0yg7IvF_Qnhqe_dcZF1swtL4Rfff3ZWo1ESxkg6SfwTO0urqaw3lOpIDtfoTKh6_VVPOsaRAYfRzy6lwQ1mAmBU57EMdyjZTrb7hiBi8wWPY2bDxREn9s6zFoDGY3yvr3tsD1HbERn2TQ5loOYaUkEX2t_1yB34kbBMnyp30zZkcktoahCKDFw/savebootstrap.png)
