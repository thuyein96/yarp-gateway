# API Gateway Implementation with YARP

This project demonstrates a practical implementation of an **API Gateway** using **YARP (Yet Another Reverse Proxy)** in C#. It's designed to help you understand the core concepts of API Gateways through real-world features and hands-on experience.

## 🔧 Features

- **🔁 Reverse Proxy** – Route incoming HTTP requests to appropriate backend services.  
- **⚖️ Load Balancing** – Custom load balancing based on tenant
- **🔐 API Authentication** – Secure your endpoints using authentication mechanisms.

---

## 🛠️ Implementing Reverse Proxy

**Challenge:**  
Received a `502 Bad Gateway` error when accessing services through the API Gateway.

**Solution:**  
Ensure that **all dependent projects/services are running simultaneously**. The 502 error typically occurs when the gateway cannot reach a connected service (e.g., due to it being offline or incorrectly routed).

---

Feel free to fork this project and explore how to set up a custom API Gateway using YARP!
