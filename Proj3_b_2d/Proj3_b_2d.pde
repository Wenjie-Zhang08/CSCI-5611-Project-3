//Inverse Kinematics
//CSCI 5611 IK [Solution]
// Stephen J. Guy <sjguy@umn.edu>

String windowTitle = "Project 3 Part b 2D Version";
Arm[] arms = new Arm[2];
float[] humanLen = {100f,100f,50f,25f};
//float[] startAngles = {0.3,0.3,0.3,0.3};
float[] accelerations = {0.05,0.05,0.1,0.2};

float[] maxAnglesR = {PI/8,PI/2,PI/2,PI/2};
float[] minAnglesR = {0,0,0,-PI/2};

float[] maxAnglesL = {PI,0,0,PI/2};
float[] minAnglesL = {-PI/8 + PI, -PI/2,-PI/2,-PI/2};

boolean[] angleB = {true,true,true,true};


Vec2[] center = {new Vec2(200,200),new Vec2(400,300),new Vec2(600,100)};
float[] rads = {50,50,50};


void setup(){
  size(640,480);
  surface.setTitle(windowTitle);
  arms[0]= new Arm(5);
  arms[0].setRootPosition(new Vec2(300.0f,100.0f));
  arms[0].setLens(humanLen);
  arms[0].setAngles(maxAnglesL,maxAnglesL,minAnglesL, angleB);
  arms[0].setAccs(accelerations);
  //arms[0].disableLimits();
  arms[1] = new Arm(5);
  arms[1].setRootPosition(new Vec2(300.0,100.0f));
  arms[1].setLens(humanLen);
  arms[1].setAngles(maxAnglesR,maxAnglesR,minAnglesR,angleB);
  arms[1].setAccs(accelerations);
}




void draw(){
     // noStroke();
  background(250,250,250);
  fill(214,191,105);
  //fill(200,0,180);
  for(int i = 0; i < arms.length; i ++){
    arms[i].fk();
    arms[i].solve();
    arms[i].drawArm();
  }
  for(int i = 0; i < center.length; i ++){
     if(i == 0) fill(255,0,0);
     else fill (0,255,0);
      ellipse(center[i].x,center[i].y,2*rads[i],2*rads[i]);
  }

}

void keyPressed(){
  if(keyCode == UP) center[0].y -= 5;
  if(keyCode == DOWN) center[0].y += 5;
    if(keyCode == LEFT) center[0].x-= 5;
  if(keyCode == RIGHT) center[0].x+= 5;
  
}


boolean jointLimited = false;

void keyReleased(){
  if(key == 'm'){
    if(jointLimited){
      for(int i = 0; i < arms.length; i ++){
        arms[i].disableLimits();
      }
      jointLimited = false;
      println("disabled limits");
    }
    else{
      for(int i = 0; i < arms.length; i ++){
        arms[i].enableLimits();
      }
      jointLimited = true;
           println("enabled limits");
    }
    
  }
}
