import Avrasya from "avrasya";

const server = new Avrasya();

server.router.get("/", (ctx) => {
    ctx.send("Wellcome to my real world");
});

server.middleware.add((ctx) => {
    console.log("middleware");
    console.log(ctx.req.url + " " + ctx.req.method);
})

server.listen(1923);