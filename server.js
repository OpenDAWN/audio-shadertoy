var io = require('socket.io').listen(1337)
  , fs = require('fs')
  , osc = require("./lib/omgosc");

io.set('log level', 1) // debug level

var receiver = new osc.UdpReceiver(1338);
receiver.on("", function(e) {
  io.sockets.emit('fft', e);
});
