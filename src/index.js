import * as Reddit from "./API/Reddit.js";

// debug and test file for when running locally to iterate fast
(async () => {
  try {
    // let res = await Redis.loadSampleData();
    // console.log("🚀 ~ file: index.js:5 ~ res:", res);

    let res = await Reddit.getTopPosts("dotnet", "year");
    console.log("🚀 ~ file: index.js:10 ~ res:", res);

    // const o = {
    //   key1: "value1",
    //   key2: "value2",
    // };

    // await Redis.SetKeyAsync("simple2", JSON.stringify(o));
  } catch (error) {
    console.log("error: ", error);
  }
})();
