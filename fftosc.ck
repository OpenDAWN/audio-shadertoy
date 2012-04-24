OscSend osc_out;
osc_out.setHost("localhost", 1338);

adc => FFT fft => blackhole;
4 => fft.size;

"" => string typetag;
while(typetag.length() < fft.size()) {
  typetag + "f" => typetag;
}

while(true) {
  fft.upchuck();

  osc_out.startMsg("/ck/fft/size", "f");
  fft.size() => osc_out.addFloat;

  osc_out.startMsg("/ck/fft", typetag);
  for(0 => int i; i < fft.size(); ++i) {
    fft.fval(i) => osc_out.addFloat;
  }

  // advance time
  100::ms => now;
}
