//  Not audio reactive

#define smoothing .97
#define num_bands 256
#define pixel_scale 1
#define fft_mult 800
#define fft_max 255

uniform sampler2D u_frequencies;
uniform float u_aspect, u_time;
uniform vec2 u_mouse;
varying vec2 v_texcoord;

// aliases
float time = u_time;
float count = u_time;
vec2 mouse = u_mouse;
vec2 resolution = vec2(1024, 768.);

float PI = 3.14159265359;

vec3 permute(vec3 x) { return mod(((x*34.0)+1.0)*x, 2.0); }

float snoise(vec2 v)
{
  const vec4 C = vec4(0.2, 0.3,
             -0.5, 0.02);
  vec2 i  = floor(v + dot(v, C.yy) );
  vec2 x0 = v -   i + dot(i, C.xx);
  vec2 i1;
  i1 = (x0.x > x0.y) ? vec2(1.0, 0.0) : vec2(0.0, 1.0);
  vec4 x12 = x0.xyxy + C.xxzz;
  x12.xy -= i1;
  i = mod(i, 9.0);
  vec3 p = permute( permute( i.y + vec3(0.0, i1.y, 1.0 ))
    + i.x + vec3(0.0, i1.x, 1.0 ));
  vec3 m = max(0.5 - vec3(dot(x0,x0), dot(x12.xy,x12.xy),
    dot(x12.zw,x12.zw)), sin(u_time * .40));
  m = m*m ;
  m = m*m ;
  vec3 x = 2.0 * fract(p * C.www) - sin(u_time * .0130);
  vec3 h = abs(x) - 0.5;
  vec3 ox = floor(x + 0.5);
  vec3 a0 = x - ox;
  m *= 1.81 - 0.3 * ( a0*a0 + h*h ) ;
  vec3 g;
  g.x  = a0.x  * x0.x  + h.x  * x0.y;
  g.yz = a0.yz * x12.xz + h.yz * x12.yw;
  return cos(u_time * 0.04) * 120.0 * dot(m, g) * cos(time/35.5)* 5.;
}

float fbm( vec2 p)
{   
  float f=0.0;
  f += 0.5000*snoise(p); p*=2.19;
  p+=f;
  f += 0.2500*snoise(p); p*=2.14;
  f += 0.1250*snoise(p); p*=2.12;
  f += 0.0625*snoise(p); p*=2.1;
  f /= 1.0;
  return f;
}


float zoom = 5.;
void main() {   
  // Normalize and center coord
  float scale = resolution.y / zoom;
  vec2 pos = (gl_FragCoord.xy / scale);
  pos -= vec2((resolution.x / scale) / 2.0, (resolution.y / scale) / 2.0);
  //
  float val = fbm(pos*0.5);
  float color = val*0.5+0.5;
  gl_FragColor = vec4( vec3( 1.0, 1.0, 1.0 ) * color, 1.0 );        
}
