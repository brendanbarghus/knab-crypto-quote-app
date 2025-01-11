# Crypto Quote Application

This is a simple ASP.NET Core MVC web application that provides live cryptocurrency quotes for various currencies. The application allows users to input a cryptocurrency code (like BTC or ETH) and get the latest price data for that cryptocurrency.

### Features

*   **Cryptocurrency Quote:** Enter a cryptocurrency code (e.g., BTC, ETH) and get the latest quote.
*   **Real-Time Data:** Quotes are fetched in real-time from the configured cryptocurrency data source (e.g., an API).
*   **Responsive UI:** The app uses Bootstrap for a responsive design that works on both desktop and mobile.
*   **Table View:** The results are displayed in a table format with currency values in USD or other local formats.

### Requirements

*   **.NET 8 SDK** (or later) installed on your machine.
*   A working internet connection to fetch real-time cryptocurrency data.

### Installation

#### Clone the Repository

```
git clone https://github.com/bbarghus/crypto-quote-app.git
```

#### Install Dependencies

```
dotnet restore
```

#### Running the Application

You can start the application by running the following command in the root of the project:

```
dotnet run
```

This will start the application on `https://localhost:5001`. Open this URL in your web browser to interact with the application.

### Features

## Swagger UI Integration

Swagger is integrated into the project to provide API documentation. You can use Swagger UI to interact with the API endpoints, view their descriptions, and test them directly from the browser.

### Access Swagger UI

Once the application is running locally, navigate to the following URL to access the Swagger UI:

https://localhost:5001/swagger

## User Interface:

*   **Home Page:** The home page contains a form where the user can enter a cryptocurrency code (e.g., BTC for Bitcoin or ETH for Ethereum).
*   **Quotes Table:** After submitting the form, the cryptocurrency’s quote is displayed in a table with the name of the cryptocurrency and its corresponding value.

#### Example Flow:

1.  Navigate to the homepage.
2.  Enter a cryptocurrency code like `BTC` in the input field.
3.  Click the **Get Quote** button.
4.  The page will display the cryptocurrency's price, formatted for your region (e.g., USD).

### Data Source

The application uses a third-party API namely https://exchangeratesapi.io and https://coinmarketcap.com/api (free version) to fetch live data. Ensure that the API URL and endpoint are properly configured in the app for it to work.

### Technical Details:

#### Models:

*   **CryptoQuote:** Contains the main data structure for representing cryptocurrency quotes.
*   **Quote:** Represents each individual quote, including the cryptocurrency code and its value.

#### Controllers:

*   **HomeController:** Handles the form submission, retrieves data, and passes it to the view.

#### Views:

*   **Index.cshtml:** Contains the form for user input and displays the resulting table of quotes.
*   **Readme.cshtml:** Displays the README file’s content (used as a static page in the web app).

### API Integration

The app makes a GET request to a cryptocurrency API (e.g., CoinGecko or any custom API) to fetch live data. Ensure that the API URL and endpoint are properly configured in the app for it to work.

### Tests

#### Unit Tests:

We use **xUnit** for testing. Unit tests are available for checking the data retrieval from the API and formatting of the quote values.

#### UI Tests:

Use **Selenium** or other UI test libraries to verify form submission, display of quotes, and error handling.

#### Running Tests:

```
dotnet test
```

### Error Handling

*   **Invalid Cryptocurrency Code:** If the user enters an incorrect or unsupported code, the application displays an error message.
*   **API Failure:** If the external API fails, an error message is shown to the user on screen.

### License

This application is licensed under the Knab License.

### Contact

For any questions, please contact [bbarghus@gmail.com](mailto:bbarghus@gmail.com).

### Acknowledgments

*   This project is built using **ASP.NET Core**.
*   Special thanks to the [Exchangerates API](https://exchangeratesapi.io/) and [Coinmarketcap API](https://coinmarketcap.com/api) for providing free cryptocurrency data.
*   **Bootstrap 5** for responsive web design.