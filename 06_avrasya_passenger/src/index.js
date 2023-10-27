import avrasya from "avrasya";

var server = new avrasya();

server.router.get("/", (ctx) => {
    ctx.send("Wellcome to my real world");
});

server.middleware.add((ctx) => {
    console.log("middleware");
    console.log(ctx.req.url + " " + ctx.req.method);
})

server.listen(1923);