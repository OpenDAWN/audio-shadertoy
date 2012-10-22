#define fft_mult 80000
#define fft_max 255
#define smoothing .9572
#define pixel_scale 1

uniform sampler2D u_frequencies;
uniform float u_aspect, u_time;
uniform vec2 u_mouse;
varying vec2 v_texcoord;

#define PI 3.141592653589793

void main(){

  vec2 pos = vec2((v_texcoord.x * 3.0 - 1.5) * u_aspect, v_texcoord.y * 2.8 - 1.4);
  float theta = abs(atan(pos.x, pos.y)) / PI * 1.81;
  float amp = texture2D(u_frequencies, vec2(0.427, 0.4)).x * 13.60; // <-- start
  float dist = distance(pos, vec2(0., 0.1));
  dist *= dist;

  float fade = float(amp > dist * .310) * (dist);

  vec2 resolution = vec2(2.0, 2.0);

  // speed
  vec2 p = pos * sin(u_time / 2.3) * sin(u_time * 0.4);// * 18.4; <---- patterns
  
  const float tot = PI * 3.00;
  const float n = 8.0;
  const float df = tot / n * 0.3;

  // speed
  float c = 2.0  * 10.0 * (sin(u_time / 0.003) / 50.0);

  float t = u_time * (theta / 2.5 * sin(u_time * .2)) * (amp * 5.10) / 30000.;

  if (n > 1.0) {
    for (float phi = 0.0; phi < tot; phi += df){
      c += cos(cos(phi) * p.x+ sin(phi) * p.y + t) * fade;
    }
  }
  c /= n;
//  c = c > 0.05 ? 1.0 : 0. ;
  
  gl_FragColor = vec4(c, c, c, 1.);
}
