import avrasya from "avrasya";

const server = new avrasya.default();

server.router.get("/product/:id", (ctx) => {
    console.log(ctx.params["id"]);
    ctx.send(ctx.params["id"] + " product infos");
});

server.router.post("/product", (ctx) => {
    console.log(ctx.body);
    ctx.send("Post request");
})

server.router.delete("/product/id", (ctx) => {
    ctx.send("Delete request");
})

server.router.put("/product", (ctx) => {
    console.log(ctx.body);
    ctx.send("Put request");
})

server.use((ctx) => {
    console.log("middleware");
    console.log(ctx.req.url + " " + ctx.req.method);
})

server.listen(1923);