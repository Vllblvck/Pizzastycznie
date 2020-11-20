# Pizzastycznie  
Web API and frontend written in .NET Core for placing orders in a restaurant.  

# Features  
- Ordering any set of dishes 'n' times  
- Order confirmation email sending to customer  
- Order history  
- User authentication and roles  
- API for administrator panel (Modyfing restaurant's menu, changing orders status, listing pending orders)    

# How to run backned on server    
- Configure SmtpSettings section in appsettings.json (required for email sending)  
- Set CONNECTION_STRING environment variable in Dockerfile (database connection)  
- Set SECURITY_KEY environment variable in Dockerfile (used for authentication)
- Build and run the Docker image  

# Possible improvements  
- Frontend doesn't allow all the things that API does
- Frontend is ugly :(  
- Frontend could be a website or mobile app  
