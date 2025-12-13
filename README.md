# reddit-server

To start...
run: "docker-compose up -d"

# URLs

API Health:

- http://localhost:3001

Samples:

- Random:
  - http://localhost:3001/random?subreddit=hiking&range=day
  - http://localhost:3001/random?subreddit=dotnet&range=year
- Top:
  - http://localhost:3001/top?subreddit=learnmachinelearning&range=day
  - http://localhost:3001/top?subreddit=hiking&range=year

Redis:

- http://localhost:6379

RabbitMQ Manager:

- http://localhost:15672/
