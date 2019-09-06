# Comment Actualizer - a tool to view and analyze youtube comments and maybe make some reports?

## API Functional requirements/controllers

1. Make a request for the first 100 comments of any youtube video. 
2. Make requests for the next 100 comments of a youtube vido using the nextpagetoken to pass in to attain the other comments. 
3. Make request for comments that match a certain parameter, allow to view comment data (e.g. likes, replies counts) and link to comment.
       https://www.googleapis.com/youtube/v3/commentThreads?part=snippet&maxResults=100&searchTerms=dog&textFormat=plainText&videoId=XXzbyUCtL2o&key=[YOUR_API_KEY]
4. Allow users to send JSON to controller to analyze sentiment. -- need to use VaderSharp for this..
