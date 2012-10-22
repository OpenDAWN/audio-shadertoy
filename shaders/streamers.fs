#define smoothing .950
#define num_bands 256
#define pixel_scale 2
#define fft_mult 80000
#define fft_max 255

uniform sampler2D u_frequencies;
uniform float u_aspect, u_time;
uniform vec2 u_mouse;
varying vec2 v_texcoord;

// glsl converstions
float time = u_time;
vec2 mouse = u_mouse;
float count = u_time;
vec2 resolution = vec2(1024/2,768.2);

void main( void )
{

    vec2 uPos = ( gl_FragCoord.xy / resolution.xy );//normalize wrt y axis
    //uPos -= vec2((resolution.x/resolution.y)/2.0, 0.0);//shift origin to center
    
// left/right
    uPos.x -= 0.5 + (sin(u_time * 0.2) * 0.25);
    
    float vertColor = 0.0;

   // num lines ------------|
    for( float i = 0.0; i < 7.0; ++i )
    {
        float t = time * (i + 0.001 * uPos.y * uPos.x);
        uPos.x += cos( uPos.y + t ) * .1080;
        float fTemp = abs(1.0 / uPos.x / (sin(u_time*10.3) + 50.));
        vertColor += fTemp;
    }
    
    vec4 color = vec4( 
    vertColor * sin(time * 0.3), 
    vertColor * 0.15, 
    vertColor * 12.5, 1.0 );
    gl_FragColor = color;
}
