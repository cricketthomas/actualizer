# Actualizer - a simple Vue PWA to fetch youtube comments!


## Screenshots:







This is a small tool to fetch youtube comments, you also get sentiment from Vader NLP, and get commonly used words (and make a word cloud!).

Ideally, it would have been an analytics dashboard and cools charts to explore these comments, however, this project was started to improve/practice the C# language, I evolved it into a web API using .NET Core 3.1.2 (.net is nice!), added some Microsoft libraries, like JSON.NET, added a DB using SQLite and Entity Framework and just tried to get more comfortable with this ecosystem.

After making it an API, I thought why not add in a Frontend, using the greatest Frontend framework available, Vue.js. I tried to structure it like a big project, and it just turned out to be an overcomplicated mess, but I learned something, how to do things, and how not to. I added some libraries and tried yet another CSS and Component framework, of Bulma and Buefy, and they are both great and I will be advocating for them in the future. I played around with the composition API and what the future holds for Vue, which looks awesome. I added some stuff to make it look like a Native app (by making it a Progressive Web App/PWA).


Finally, I threw it up it in Azure, with the frontend and Backend being built through Azure DevOps, then deployed to Azure App Service windows (for node and IIS) and Azure App Service Linux for .Net


```
This app was mostly me just playing around with .NET 3.1, SQLite, Vue.js, and Azure Devops.
```
