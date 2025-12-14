import amqp from "amqplib";
import { info, warn, error, debug } from "../Logger/Logger.js";

const RABBIT_URL = process.env.RABBITMQ_URL;

// https://www.rabbitmq.com/tutorials/tutorial-one-javascript
export const sendMessage = async (message, queue_name) => {
  try {
    debug("connecting to rabbitmq");
    const connection = await amqp.connect(RABBIT_URL);
    debug("connected to rabbitmq");

    debug("creating channel");
    const channel = await connection.createChannel();
    debug("channel created");

    connection.on("open", () => console.log("connection opened"));
    connection.on("close", () => console.log("connection closed"));
    connection.on("error", (e) => error("connection error:", e.message));

    await channel.assertQueue(queue_name, { durable: true });

    let data = "";
    if (typeof message === "string") {
      data = message;
    } else {
      data = JSON.stringify(message);
    }

    channel.sendToQueue(queue_name, Buffer.from(data), { persistent: true });
    info("🚀 ~ sendMessage ~ Sent message to queue:", message);

    await channel.close();
    await connection.close();
  } catch (e) {
    error(e.message);
  }
};
