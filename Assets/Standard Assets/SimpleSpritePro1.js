/* SimpleSprite Pro Version 1.0
   
   Copyright (c) 2012 Black Rain Interactive
   All Rights Reserved
*/
//import UnityEngine;
#pragma strict

// Setup Animations
var animation0 : Texture[];
var animation1 : Texture[];
var animation2 : Texture[];
var animation3 : Texture[];
var animation4 : Texture[];
var animation5 : Texture[];
var animation6 : Texture[];
var animation7 : Texture[];
var animation8 : Texture[];
var animation9 : Texture[];
var animation10 : Texture[];
var animation11 : Texture[];
var animation12 : Texture[];
var animation13 : Texture[];
var animation14 : Texture[];
var usesGui : boolean = false;

var loop : boolean = true;

var guiPosition : Vector2;
var guiSize : Vector2;
var animationSpeed : float = 10;
var billboard : boolean = false;
var playAutomatically : boolean = false;

private var loopStop : boolean = false;
private var timeAlive : float;
// Animation To Play
private var animationPlay : int;
private var cameraToLookAt : Camera;
private var guiTex : Texture;

// Sets The Billboading Variable
function Awake()
{ 
  cameraToLookAt = Camera.main;
   
   if (playAutomatically == true && usesGui == false){
      PrePlay();
      PlayAnimation(0);
   }
   
   if (playAutomatically == true && usesGui == true){
      PrePlay();
      PlayGUI(0);
   }
}

function Update(){   
// Activate Billboard
	timeAlive += Time.deltaTime;
   if (billboard == true){
        var v : Vector3 = cameraToLookAt.transform.position - transform.position;
        v.x = v.z = 0.0;
        transform.LookAt(cameraToLookAt.transform.position - v); 
   }
}

// Prepare To Play Animation
function PrePlay() 
{  
   StopAllCoroutines();
}
function PreNextAnimation()
{
	yield WaitForSeconds(0.0001f);
}
function ChangeAnimation(animationIndex : int)
{
	animationPlay = animationIndex;
	timeAlive = 0;
}
// Plays The Selected Animation
function PlayAnimation(animationIndex : int){
 
  loopStop = false;
  timeAlive = 0; //Helps Maintain animation integrity, always starting and ending correctly
  
  animationPlay = animationIndex;

while (true &&   !loopStop ){  
if (animationPlay == 0){
   // Play Animation0
   var index0 : int = timeAlive * animationSpeed;
   index0 = index0 % animation0.length;
   renderer.material.mainTexture = animation0[index0];
   
   if(!loop && index0 == animation0.Length-1)
   {
   		loopStop = true;
   		//PrePlay();
   }
}
 
if (animationPlay == 1){  
   // Play Animation1
   var index1 : int = timeAlive * animationSpeed;
   index1 = index1 % animation1.length;
   renderer.material.mainTexture = animation1[index1];
   
   if(!loop && index1 == animation1.Length-1)
   {
   		loopStop = true;
   }
   }
   
if (animationPlay == 2){
   // Play Animation2
   var index2 : int = timeAlive * animationSpeed;
   index2 = index2 % animation2.length;
   renderer.material.mainTexture = animation2[index2];
   
   if(!loop && index2 == animation2.Length-1)
   {
   		loopStop = true;
   }
   }
   
if (animationPlay == 3){

   // Play Animation3
   var index3 : int = timeAlive * animationSpeed;
   index3 = index3 % animation3.length;
   renderer.material.mainTexture = animation3[index3];
   if(!loop && index3 == animation3.Length-1)
   {
   		loopStop = true;
   }
   }
   
if (animationPlay == 4){
   // Play Animation4
   var index4 : int = timeAlive * animationSpeed;
   index4 = index4 % animation4.length;
   renderer.material.mainTexture = animation4[index4];
   if(!loop && index4 == animation4.Length-1)
   {
   		loopStop = true;
   }
   }
   
if (animationPlay == 5){
   // Play Animation5
   var index5 : int = timeAlive * animationSpeed;
   index5 = index5 % animation5.length;
   renderer.material.mainTexture = animation5[index5];
   
   if(!loop && index5 == animation5.Length-1)
   {
   		loopStop = true;
   }
   }
   
if (animationPlay == 6){
   // Play Animation6
   var index6 : int = timeAlive * animationSpeed;
   index6 = index6 % animation6.length;
   renderer.material.mainTexture = animation6[index6];
   
   if(!loop && index6 == animation6.Length-1)
   {
   		loopStop = true;
   }
   }
   
if (animationPlay == 7){
   // Play Animation7
   var index7 : int = timeAlive * animationSpeed;
   index7 = index7 % animation7.length;
   renderer.material.mainTexture = animation7[index7];
   
   if(!loop && index7 == animation7.Length-1)
   {
   		loopStop = true;
   }
   }
   
if (animationPlay == 8){
   // Play Animation8
   var index8 : int = timeAlive * animationSpeed;
   index8 = index8 % animation8.length;
   renderer.material.mainTexture = animation8[index8];
   
   if(!loop && index8 == animation8.Length-1)
   {
   		loopStop = true;
   }
   }
   
if (animationPlay == 9){
   // Play Animation9
   var index9 : int = timeAlive * animationSpeed;
   index9 = index9 % animation9.length;
   renderer.material.mainTexture = animation9[index9];
   if(!loop && index9 == animation9.Length-1)
   {
   		loopStop = true;
   }
   }
if (animationPlay == 10){
   // Play Animation0
   var index10 : int = timeAlive * animationSpeed;
   index10 = index10 % animation10.length;
   renderer.material.mainTexture = animation10[index10];
   
   if(!loop && index10 == animation10.Length-1)
   {
   		loopStop = true;
   }
   }
if (animationPlay == 11){
   // Play Animation0
   var index11 : int = timeAlive * animationSpeed;
   index11 = index11 % animation11.length;
   renderer.material.mainTexture = animation11[index11];
   
   if(!loop && index11 == animation11.Length-1)
   {
   		loopStop = true;
   }
   }
if (animationPlay == 12){
   // Play Animation0
   var index12 : int = timeAlive * animationSpeed;
   index12 = index12 % animation12.length;
   renderer.material.mainTexture = animation12[index12];
   
   if(!loop && index12 == animation12.Length-1)
   {
   		loopStop = true;
   }
   }
   
if (animationPlay == 13){
   // Play Animation0
   var index13 : int = timeAlive * animationSpeed;
   index13 = index13 % animation13.length;
   renderer.material.mainTexture = animation13[index13];
   
   if(!loop && index13 == animation13.Length-1)
   {
   		loopStop = true;
   }
   }
if (animationPlay == 14){
   // Play Animation0
   var index14 : int = timeAlive * animationSpeed;
   index14 = index14 % animation14.length;
   renderer.material.mainTexture = animation14[index14];
   
   if(!loop && index14 == animation14.Length-1)
   {
   		loopStop = true;
   }
   }
      yield WaitForSeconds (0);
 }
   //yield;
}

function OnGUI(){
   
   if (usesGui){
      GUI.Label (Rect (guiPosition.x, guiPosition.y, guiSize.x, guiSize.y),guiTex);
   }
}

// The New Animated GUI Function
function PlayGUI(animationPlay : int){

	loopStop = false;
	timeAlive = 0;

while (usesGui && !loopStop){
   
if (animationPlay == 0){
   // Play GUI Animation0
   var index0 : int = timeAlive * animationSpeed;
   index0 = index0 % animation0.length;
   guiTex = animation0[index0];
   
   if(!loop && index0 == animation0.Length-1)
   {
   		loopStop = true;
   }
   }
 
if (animationPlay == 1){  
   // Play GUI Animation1
   var index1 : int = timeAlive * animationSpeed;
   index1 = index1 % animation1.length;
   guiTex = animation1[index1];
   }
   
if (animationPlay == 2){
   // Play GUI Animation2
   var index2 : int = timeAlive * animationSpeed;
   index2 = index2 % animation2.length;
   guiTex = animation2[index2];
   }
   
if (animationPlay == 3){
   // Play GUI Animation3
   var index3 : int = timeAlive * animationSpeed;
   index3 = index3 % animation3.length;
   guiTex = animation3[index3];
   }
   
if (animationPlay == 4){
   // Play GUI Animation4
   var index4 : int = timeAlive * animationSpeed;
   index4 = index4 % animation4.length;
   guiTex = animation4[index4];
   }
   
if (animationPlay == 5){
   // Play GUI Animation5
   var index5 : int = timeAlive * animationSpeed;
   index5 = index5 % animation5.length;
   guiTex = animation5[index5];
   }
   
if (animationPlay == 6){
   // Play GUI Animation6
   var index6 : int = timeAlive * animationSpeed;
   index6 = index6 % animation6.length;
   guiTex = animation6[index6];
   }
   
if (animationPlay == 7){
   // Play GUI Animation7
   var index7 : int = timeAlive * animationSpeed;
   index7 = index7 % animation7.length;
   guiTex = animation7[index7];
   }
   
if (animationPlay == 8){
   // Play GUI Animation8
   var index8 : int = timeAlive * animationSpeed;
   index8 = index8 % animation8.length;
   guiTex = animation8[index8];
   }
   
if (animationPlay == 9){
   // Play GUI Animation9
   var index9 : int = timeAlive * animationSpeed;
   index9 = index9 % animation9.length;
   guiTex = animation9[index9];
   }
if (animationPlay == 10){
   // Play GUI Animation5
   var index10 : int = timeAlive * animationSpeed;
   index10 = index10 % animation10.length;
   guiTex = animation10[index10];
   }
   
if (animationPlay == 11){
   // Play GUI Animation6
   var index11 : int = timeAlive * animationSpeed;
   index11 = index11 % animation11.length;
   guiTex = animation11[index11];
   }
   
if (animationPlay == 12){
   // Play GUI Animation7
   var index12 : int = timeAlive * animationSpeed;
   index12 = index12 % animation12.length;
   guiTex = animation12[index12];
   }
   
if (animationPlay == 13){
   // Play GUI Animation8
   var index13 : int = timeAlive * animationSpeed;
   index13 = index13 % animation13.length;
   guiTex = animation13[index13];
   }
   
if (animationPlay == 14){
   // Play GUI Animation9
   var index14 : int = timeAlive * animationSpeed;
   index14 = index14 % animation14.length;
   guiTex = animation14[index14];
   }
      yield WaitForSeconds (0);
 }
   //yield;
}

// Sets The Animation Speed
function SetSpeed(speed : float){
   animationSpeed = speed;
}