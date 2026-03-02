Approximately estimates the composition and nutritional value of a dish based on a photo.
Just add you Google API key into appsettings.json and press F5. This will run Swagger UI (http://localhost:YOUR_PORT/swagger/index.html), where you can test your food images and add some description (f.e. if something hidden under souse).

If you want to test API remotly (f.e. via mobile device), setup DevTunnel. On Windows run 'winget install Microsoft.DevTunnel', then login 'devtunnel user login' in PS. Then run API (F5) and run tunnel 'devtunnel host -p YOUR_PORT --allow-anonymous'. Then API should be accasseble remotly by address like https://RANDOMCODE-YOUR_PORT.euw.devtunnels.ms/swagger/index.html
