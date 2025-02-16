# Content Platform Messaging Queue Example

A simple example demonstrating communication between two microservices using RabbitMQ and MassTransit.

## 🚀 Stack
- .NET 8
- RabbitMQ
- MassTransit
- EFCore
- PostgreSQL

## 🏗️ Workflow
We have two microservices:

### 1️⃣ Newsletter Articles
- Responsible for creating articles and allowing users to like or dislike them.

### 2️⃣ Newsletter Analytics
- Responsible for tracking and storing article interactions (likes and dislikes).

### 🔄 Process Flow
1. A user likes or dislikes an article via the **Articles API**.
2. The **Articles API** sends an event to the **RabbitMQ** queue.
3. The **Analytics API** consumes the event and updates its database with the new interaction count.
4. The **Analytics API** sends a message back to the **Articles API** with the updated total interactions.
5. The **Articles API** updates the displayed total like/dislike count for the article.

## 🎯 Why This Approach?
The goal is to offload the interaction counting process to a separate service, allowing the **Articles API** to focus on handling article creation and user interactions efficiently. The **Analytics API** takes on the heavy lifting of counting and storing interactions.

> **Note:** This example is intentionally simplistic and overkill for a real-world scenario. However, it serves as a great way to demonstrate message queues in action. In a real-world system, this approach is more suited for high-traffic services like payment processing or handling technical debt in a microservices architecture.

## 📌 Disclaimer
This project was built as a quick learning exercise (under 1 day). The architecture is not optimized for production but is meant to illustrate message queue concepts.
