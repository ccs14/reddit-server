import amqp from "amqplib";

const RABBIT_URL = "amqp://rabbitmq";
const QUEUE_NAME = "reddit_queue";

// https://www.rabbitmq.com/tutorials/tutorial-one-javascript
export const sendMessage = async (message) => {
  try {
    const connection = await amqp.connect(RABBIT_URL);
    const channel = await connection.createChannel();

    await channel.assertQueue(QUEUE_NAME, { durable: true });

    let data = "";
    if (typeof message === "string") {
      data = message;
    } else {
      data = JSON.stringify(message);
    }

    channel.sendToQueue(QUEUE_NAME, Buffer.from(data), { persistent: true });
    console.log(`Sent: ${message}`);

    await channel.close();
    await connection.close();
  } catch (err) {
    console.error(err.message);
  }
};
