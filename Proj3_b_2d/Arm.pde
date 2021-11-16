class Arm{
 
  
  //Root
  //Vec2 root = new Vec2(0,0);
  float armW = 20;
  int numJoints = 3;
  float[] lens;
  float[] angles;
  float[] accs;
  float[] maxA;
  float[] minA;
  boolean[] angleBound;
  float[] radiis;
  //Upper Arm
  //float l0 = 100; 
  //float a0 = 0.3; //Shoulder joint
  
  //Lower Arm
  //float l1 = 75;
  //float a1 = 0.3; //Elbow joint
  
  //Hand
  //float l2 = 25;
  //float a2 = 0.3; //Wrist joint
  
  //float acc0 = 0.005;
  //float acc1 = 0.01;
  //float acc2 = 0.02;
  
  
  //Vec2 start_l1,start_l2,endPoint;

  Vec2[] jointPos;
  
  
 
  Arm(int nj){
    numJoints = nj;
    lens = new float[numJoints];
    angles = new float[numJoints];
    maxA = new float[numJoints];
    minA = new float[numJoints];
    angleBound = new boolean[numJoints];
    accs = new float[numJoints];
    jointPos = new Vec2[numJoints];
    radiis = rads.clone();
    for(int i = 0; i < radiis.length; i ++){
     radiis[i] += armW; 
      
    }
  }
  
  public void setLens(float[] ls){
    lens = ls.clone();
  }
  
  public void setAngles(float[] agls, float[] maxAng, float[] minAng, boolean[] ab){
    angles = agls.clone();
    maxA = maxAng.clone();
    minA = minAng.clone();
    angleBound = ab.clone();
    //fk();
  }
 
  public void setAccs(float[] acs){
   accs = acs.clone(); 
  }
 
  public void setRootPosition(Vec2 newPos){
    jointPos[0] = newPos;
  }
 
 public void disableLimits(){
   for(int i = 0; i < angleBound.length; i++){
     angleBound[i] = false;
   }
 }
   
  public void fk(){
    for (int i = 1; i < numJoints; i ++){
      // 0 is root
      float globalAngle = 0;
      for(int j = 0; j < i; j ++){
        globalAngle += angles[j];
      }
      Vec2 dir = new Vec2(cos(globalAngle) * lens[i-1] , sin(globalAngle) * lens[i-1]);
      
      // collision
      Vec2 startPoint = jointPos[i-1];
      hitInfo hit = rayCircleListIntersect(center,radiis ,center.length,startPoint, dir, 1.0);
      if(hit.hit){
       // we need to caculate the delta angle
       Vec2 currDir = dir.normalized();
       Vec2 dirTCenter = hit.ct.minus(startPoint);
       
       float distTCenter = dirTCenter.length();
       float safeDist = hit.radi;
       float safeToCenter = asin(safeDist/distTCenter) ;
       
       
       
       dirTCenter = dirTCenter.normalized();
       float dotProd = dot(currDir,dirTCenter);
       dotProd = clamp(dotProd,-1,1);
       float armToCenter = acos(dotProd);
       
       float deltaAngles = armToCenter - safeToCenter;
       
       
       if(cross(currDir,dirTCenter) > 0){
         
         angles[i-1] += deltaAngles;
         globalAngle += deltaAngles;
         
       }
       else{
         angles[i-1] -= deltaAngles;
         globalAngle -= deltaAngles;
         
       }
       
       dir = new Vec2(cos(globalAngle) * lens[i-1] , sin(globalAngle) * lens[i-1]);
      }
      jointPos[i] = dir.plus(jointPos[i-1]); 
      // solve collision here?
      
    }
    //  start_l1 = new Vec2(cos(a0)*l0,sin(a0)*l0).plus(root);
    //start_l2 = new Vec2(cos(a0+a1)*l1,sin(a0+a1)*l1).plus(start_l1);
    //endPoint = new Vec2(cos(a0+a1+a2)*l2,sin(a0+a1+a2)*l2).plus(start_l2);
  }
  
  
  public void solve(){
    Vec2 goal = new Vec2(mouseX, mouseY);
    
    Vec2 startToGoal, startToEndEffector;
    float dotProd, angleDiff;
    
    for(int i = numJoints - 2; i >= 0; i --){
      startToGoal = goal.minus(jointPos[i]);
      startToEndEffector = jointPos[numJoints-1].minus(jointPos[i]);
      dotProd = dot(startToGoal.normalized(),startToEndEffector.normalized());
      dotProd = clamp(dotProd,-1,1);
      angleDiff = acos(dotProd);
      if(angleDiff > accs[i]) angleDiff = accs[i];
      if (cross(startToGoal,startToEndEffector) < 0){
        angles[i] += angleDiff;
        //if(angleBound[i]){
          
        //}
        //if(a2 > PI/2) a2 = PI/2;
      }
      else{
        angles[i] -= angleDiff;

      }
      if(angleBound[i]){
          angles[i] = clamp(angles[i],minA[i],maxA[i]);
      }

      
      
      fk();
    }
    for(int i = 0; i < angles.length ; i++){
      
     print(angles[i] + " ") ;
    }
    println();
    
    /*
    
    //Update wrist joint
    startToGoal = goal.minus(start_l2);
    startToEndEffector = endPoint.minus(start_l2);
    dotProd = dot(startToGoal.normalized(),startToEndEffector.normalized());
    dotProd = clamp(dotProd,-1,1);
    angleDiff = acos(dotProd);
    if(angleDiff > acc2) angleDiff = acc2;
    if (cross(startToGoal,startToEndEffector) < 0){
      a2 += angleDiff;
      if(a2 > PI/2) a2 = PI/2;
    }
    else{
      a2 -= angleDiff;
      if(a2 < -PI/2) a2 = -PI/2;
    }
    fk(); //Update link positions with fk (e.g. end effector changed)
    
    
    //Update elbow joint
    startToGoal = goal.minus(start_l1);
    startToEndEffector = endPoint.minus(start_l1);
    dotProd = dot(startToGoal.normalized(),startToEndEffector.normalized());
    dotProd = clamp(dotProd,-1,1);
    angleDiff = acos(dotProd);
    if(angleDiff > acc1) angleDiff = acc1;
    if (cross(startToGoal,startToEndEffector) < 0)
      a1 += angleDiff;
    else
      a1 -= angleDiff;
    fk(); //Update link positions with fk (e.g. end effector changed)
    
    
    //Update shoulder joint
    startToGoal = goal.minus(root);
    if (startToGoal.length() < .0001) return;
    startToEndEffector = endPoint.minus(root);
    dotProd = dot(startToGoal.normalized(),startToEndEffector.normalized());
    dotProd = clamp(dotProd,-1,1);
    angleDiff = acos(dotProd);
    if(angleDiff > acc0) angleDiff = acc0;
    if (cross(startToGoal,startToEndEffector) < 0){
      a0 += angleDiff;
      if(a0 > PI/2) a0 = PI/2;
    }
    else{
      a0 -= angleDiff;
      if(a0 < 0) a0 = 0;
    }
    fk(); //Update link positions with fk (e.g. end effector changed)
   
    println("Angle 0:",a0,"Angle 1:",a1,"Angle 2:",a2);
    */
  }
  
  
  public void drawArm(){
    for(int i = 0; i < numJoints - 1; i++){
      pushMatrix();
      translate(jointPos[i].x,jointPos[i].y);
      circle(0,0,armW);
      float totalRotation = 0;
      for(int j = 0; j <= i; j ++){
       totalRotation += angles[j]; 
      }
      rotate(totalRotation);
      rect(0, -armW/2, lens[i], armW);
      popMatrix();

    }
    
    
    
    /*
    pushMatrix();
    translate(root.x,root.y);
    rotate(a0);
    rect(0, -armW/2, l0, armW);
    popMatrix();
    
    pushMatrix();
    translate(start_l1.x,start_l1.y);
      circle(0,0,armW);
    rotate(a0+a1);
    rect(0, -armW/2, l1, armW);
    popMatrix();
    
    pushMatrix();
    translate(start_l2.x,start_l2.y);
    //noStroke();
    circle(0,0,armW);
    rotate(a0+a1+a2);
  
    rect(0, -armW/2, l2, armW);
    popMatrix();
    */
  }
  
}
