#### Answers to technical questions.md.

1. How long did you spend on the coding assignment? What would you add to your solution if you had
   more time? If you didn't spend much time on the coding assignment then use this as an opportunity to
   explain what you would add.
```
+ I spent around 4-5 hours.
+ I would like to make the GetCryptoPriceInUsd method dynamic by passing a dynamic base currency type
+ The exchange rate requires a conversion as well due to fact that the free license does not allow for the base currency code to be changed from EUR to USD for example.
+ Use Redis or MemoryCache to cache frequently requested data, reducing API calls and improving performance.
+ Show historical data for the past day, week, month, or year using APIs that provide historical market data.
+ Allow users to input a list of cryptocurrency codes and fetch quotes for all of them simultaneously.
+ For now its important to do whats presented in the user story. Enhancements can follow with subsequent tickets.
```

2. What was the most useful feature that was added to the latest version of your language of choice?
   Please include a snippet of code that shows how you've used it.
``` 
+ In the latest version of C#, one of the most impactful features is Interpolated String Handlers i think - fery useful compared to the older way of doing things.
```
```c#
var logger = new CustomLogger();
logger.Log($"User {userName} logged in at {DateTime.Now}");
```

3. How would you track down a performance issue in production? Have you ever had to do this?
``` 
+ Yes i have. I have tracked down a performance issue using Azure application insights. 
+ In Azure its possible to manage the CPU usage, memory usage, request latency, throughput, and error rates. Autoscale when needed.
+ Use pre-configured alerts for thresholds, such as high response times or resource utilization.
```

4. What was the latest technical book you have read or tech conference you have been to? What did you
   learn?
``` 
+ At the Misrcosoft Build Event in Utrecht, Netherlands. Really enjoyed the CoPilot.
+ Copilot offers real-time code suggestions and auto-completions as you type, allowing you to write code faster.
+ It also supports learning and assists in documentation
```

5. What do you think about this technical assessment?
``` 
+ Because its a real-world Use Case it makes it very interesting.
+ Working with third-party services, parse data, and handle constraints will test your ability.
+ Requiring unit tests highlights the importance of software quality.
+ Allowing a candidate to use any frameworks, libraries, or packages gives flexibility, will enable them to demonstrate their strengths and familiarity with tools.
+ Applying error handling delivers stable applications
```
6. Please, describe yourself using JSON.
```json
{
  "name": "Brendan Barghus",
  "job_title": "Application Architect & Team Lead",
  "location": "Huizen, Netherlands",
  "contact": {
    "email": "bbarghus@gmail.com",
    "phone": "+31685128214",
    "linkedin": "https://www.linkedin.com/in/brendan-barghus-7b793337"
  },
  "skills": [
    ".Net",
    "C#",
    "ASP.Net",
    "Azure Cloud Services",
    "LINQ",
    "Biztalk",
    "SQL",
    "Biometrics Integration",
    "JQuery",
    "JavaScript",
    "React Native",
    "React JS",
    "TypeScript",
    "REST Api",
    "JWT",
    "Bearer Tokens",
    "Application Insights",
    "Code Optimization",
    "Penetration Tests",
    "More on demand..."
  ],
  "languages": [
    "English",
    "Afrikaan",
    "Dutch"
  ],
  "interests": [
    "Playing Football (Currently play for HZV De Zuidvogels)",
    "More Football",
    "Watching Liverpool FC matches",
    "Martial Arts",
    "AI",
    "Open AI",
    "Watching Discovery Channel",
    "Finding out the next big thing that will make a difference in peoples lives"
  ]
}
