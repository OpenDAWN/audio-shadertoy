var io = require('socket.io').listen(1337)
  , fs = require('fs')
  , osc = require("./lib/omgosc");


io.sockets.on('connection', function (socket) {
  socket.emit('test', { hello: 'world' });
  socket.on('my other event', function (data) {
    console.log(data);
  });
});


// OSC

var receiver = new osc.UdpReceiver(1338);
receiver.on("", function(e) {
  console.log(e);
});
