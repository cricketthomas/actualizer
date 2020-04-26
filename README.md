# Comment Actualizer - a very simple tool to view and analyze youtube comments.
### Just playing around with .NET core and VueJS.


#### Functionality (some of which may not exist):
1. Login with Okta and other social websites. This allows you to view data you saved to the SQLite file..lol.
2. Request comment indinividually or up to 20 pages or 2000 comments. Per request.
3. Analyze comments using the Azure Cognitive API, for sentiment, key phrases and entity extraction. 
       - Peruse through the comments and the response data.. -- crap idea
       - View the aggreegated data in charts. Overall sentiment, sentiment overtime, sentiment and comment like count, key phrases                  aggreagations and be able to make a wordcloud for the key phrases.
       - Print out a nice little dashboard.
4. Request comments with a search parameter then search through those comments.


```
TODO:
Frontend:
1. Use vue composition api/hooks to reuse components for fetching the data.
2. Resuse for the custom frappe charts wrapper I will need to make for this. 

Backend:
Allow users to request different type of aggregations of the comments from the /api/comments/bulk. So they can request different sorting or summarizations?

This app is being built. poorly.```
