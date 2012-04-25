var io = require('socket.io').listen(1337)
  , fs = require('fs')
  , osc = require("./lib/omgosc")
  , spawn = require("child_process").spawn;

io.set('log level', 1) // debug level

var receiver = new osc.UdpReceiver(1338);
receiver.on("", function(e) {
  io.sockets.emit('fft', e);
});

var chuck = spawn("chuck", [ "fftosc.ck" ]);
