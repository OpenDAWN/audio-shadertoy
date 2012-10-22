#define smoothing 1.50
#define num_bands 256
#define pixel_scale 2
#define fft_mult 800
#define fft_max 255

uniform sampler2D u_frequencies;
uniform float u_aspect, u_time;
uniform vec2 u_mouse;
varying vec2 v_texcoord;

// glsl converstions
float time = u_time;
vec2 mouse = u_mouse;
float count = u_time;
vec2 resolution = vec2(600.,500.3);


void main( void ) {

    float speed = time * 0.25;
    
    vec2 position = ( (gl_FragCoord.xy - resolution.xy*0.5) / resolution.x );
    
// movement
    vec2 center1 = vec2(sin(speed), cos(speed * 0.535)) * 0.3;
    vec2 center2 = vec2(sin(speed * 0.259), cos(speed*0.1605)) * 0.4;
    vec2 center3 = vec2(sin(speed * 0.346), sin(speed*0.1263)) * 01.3;
    
// pattern size
    float size = 22.0; // (sin(time*0.1)+1.2)*222.20;
    vec2 color = vec2(0.);
    
    float d = distance(position, center1) * size;
    color += vec2(cos(d),sin(d));
    
    d = distance(position, center2)*size;
    color += vec2(cos(d),sin(d));
    
    d = distance(position, center3)*size;
    color += vec2(cos(d),sin(d)); 
    
    // color += vec2(cos(d));
    vec2 ncolor = normalize(color);
    
    vec3 clr = vec3(1.);//vec3(ncolor.x, ncolor.y, 1.); //vec3(ncolor.x, ncolor.y, -ncolor.x-ncolor.y);
    clr *= sqrt(color.x*color.x+color.y*color.y)*0.35;
    
    gl_FragColor = vec4(clr, 1.0 );
}
