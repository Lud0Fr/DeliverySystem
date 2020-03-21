# DeliverySystem - Instructions

1. Update the database connection string in appsettings.json ConnectionStrings.AppEntities
2. Run Update-Database from the Package Manager Console on DeliverySystem.Infrastructure to create the database from the migration configuration
3. Login Accounts:
	Based on the authorization, get the jwt to add to the Authorization header of each request
	- Admin: Id: 1, Email: admin@admin.com, Password: password
	- Partner: Id: 2, Email: partner@partner.com, Password: password, PartnerId: 222
	- User consumer market: Id: 3, Email: user@user.com, Password: password, UserConsumerMarketId: 333
4. API Endpoints:
	- Sigin:
		POST https://localhost:44321/api/Identity/signin
		{
			"email": "admin@admin.com",
			"password": "password"
		}
	- Create Delivery:
		POST https://localhost:44321/api/delivery
		{
			"accessWindow": {
				"startTime": "2020-03-23 10:00:00",
				"endTime": "2020-03-23 19:00:00"
			},
			"order" : {
				"orderNumber": "123456",
				"sender": "Ikea"
			},
			"recipient": {
				"name": "recipient name",
				"address": "address",
				"email": "recipient@domain.com",
				"phoneNumber": "+447000000000"
			},
			"userId": 333,
			"partnerId": 222
		}
		Authorization: Admin
	- Get Delivery
		GET https://localhost:44321/api/delivery/{deliveryId}
		Authorization: Admin,Partner, User Consumer Market
	- Get Deliveries
		GET https://localhost:44321/api/delivery
		Authorization: Admin,Partner, User Consumer Market
	- Approve Delivery:
		PUT https://localhost:44321/api/delivery/{deliveryId}/approve
		Authorization: User Consumer Market
	- Complete Delivery
		PUT https://localhost:44321/api/delivery/{deliveryId}/complete
		Authorization: Partner
	- Cancel Delivery
		PUT https://localhost:44321/api/delivery/{deliveryId}/cancel
		Authorization: Partner, User Consumer Market
	- Delete Delivery
		DELETE https://localhost:44321/api/delivery/{deliveryId}
		Authorization: Admin
		
5. Delivery Web Worker: Run the Web Worker to check every minute if a delivery expired and update its state
	
