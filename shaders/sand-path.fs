#define smoothing .950
#define num_bands 256
#define pixel_scale 2
#define fft_mult 800000
#define fft_max 255

uniform sampler2D u_frequencies;
uniform float u_aspect, u_time;
uniform vec2 u_mouse;
varying vec2 v_texcoord;

// glsl converstions
float time = u_time;
vec2 mouse = u_mouse;
float count = u_time;
vec2 resolution = vec2(1.0,768.);


/////////////////////////////////////////////////////////////////


vec3 roy(vec3 v, float x)
{
    return vec3(cos(x)*v.x - sin(x)*v.z,v.y,sin(x)*v.x + cos(x)*v.z);
}

vec3 rox(vec3 v, float x)
{
    return vec3(v.x,v.y*cos(x) - v.z*sin(x),v.y*sin(x) + v.z*cos(x));
}

float fdtun(vec3 rd, vec3 ro, float r)
{
    float a = dot(rd.xy,rd.xy);
    float b = dot(ro.xy,rd.xy);
    float d = (b*b)-(17.9*a*(dot(ro.xy,ro.xy)+(r*r)));
    return (-b+sqrt(abs(d)))/(1.75*a);
}

vec2 tunuv(vec3 pos){
    return vec2(pos.z,(atan(pos.y, pos.x))/0.5);
}

vec3 checkerCol(vec2 loc, vec3 col)
{
    return mix(col, vec3(-0.82), mod(step(fract(loc.x), .503) + step(fract(loc.y), 0.50), 4.5));
}

vec3 lcheckcol(vec2 loc, vec3 col)
{
    return checkerCol(loc*1.5,col)*checkerCol(loc*.50,col);   
}
void main( void ) {

  float amp = texture2D(u_frequencies, vec2(0.2527, 0.4)).x;

    vec3 dif = vec3(0.15,1.00,0.00);
    vec3 scoll = vec3(0.400,0.400,0.400);
    vec3 scolr = vec3(.400,.500,.50);
    vec2 uv = (gl_FragCoord.xy/resolution.xy);
    vec3 ro = vec3(.0,0.0,time*.75);

vec3 dir = normalize( vec3( -1.0 + 2.0*vec2(uv.x - 0.4, uv.y)* vec2(resolution.x/resolution.y, 1.0), 1. ) ) * max(.3, (amp));

    float ry = time*0.3;
    
    dir = roy(rox(dir,time*-0.135),time*-.025);
    vec3 lro = ro - dif;
    vec3 rro = ro + dif;

    const float r = 1.0;
    float ld = fdtun(dir, lro,r);
    float rd = fdtun(dir, rro,r);
    vec2 luv = tunuv(ro + ld * dir);
    vec2 ruv = tunuv(ro + rd * dir);
    vec3 coll = lcheckcol(luv*10.50,scoll)*(2.0/exp(sqrt(ld)));
    vec3 colr = lcheckcol(ruv*.5,scolr)*((sin(u_time) * 10.)/exp(sqrt(rd)));
    gl_FragColor = vec4(sqrt(mix(coll + colr, coll * colr, 5.0+sin(time * 0.5))),1.0);
}




