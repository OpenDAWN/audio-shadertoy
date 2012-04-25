OscSend osc_out;
osc_out.setHost("localhost", 1338);

adc => FFT fft => blackhole;
256 => fft.size;

"" => string typetag;
while(typetag.length() < fft.size()) {
  typetag + "f" => typetag;
}

while(true) {
  fft.upchuck();

  osc_out.startMsg("/ck/fft", typetag);
  for(0 => int i; i < fft.size(); ++i) {
    fft.fval(i) => osc_out.addFloat;
  }

  // advance time
  (1000/60)::ms => now;
}
