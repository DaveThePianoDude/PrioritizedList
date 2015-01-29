<?xml version="1.0" encoding="UTF-8"?>
<xsl:stylesheet version="1.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform">
   
<xsl:output method="text"/>
<xsl:strip-space elements="*"/>
     
<xsl:template match="/action" priority="10">
     <xsl:value-of select="@name"/><xsl:text> is the main action.</xsl:text>
     <xsl:call-template name="entails"/>
     <xsl:apply-templates/>
</xsl:template>
     
<xsl:template match="action[action]">
     <xsl:value-of select="@name"/><xsl:text> entails... </xsl:text>
     <xsl:call-template name="entails"/>
     <xsl:apply-templates/>
</xsl:template>
   
<xsl:template match="action">
     <xsl:value-of select="@name"/><xsl:text> has no worries. </xsl:text>
     <xsl:text>&#xa;&#xa;</xsl:text>
</xsl:template>
     
<xsl:template name="entails">
     <xsl:for-each select="*">
          <xsl:choose>
            <xsl:when test="position( ) != last( ) and last( ) > 2">
              <xsl:value-of select="@name"/><xsl:text>, </xsl:text>
            </xsl:when>
            <xsl:when test="position( ) = last( ) and last( ) > 1">
              <xsl:text> and </xsl:text><xsl:value-of 
                         select="@name"/>
              <xsl:text>. </xsl:text>
            </xsl:when>
            <xsl:when test="last( ) = 1">
              <xsl:value-of select="@name"/>
              <xsl:text>. </xsl:text>
            </xsl:when>
            <xsl:otherwise>
              <xsl:value-of select="@name"/>
            </xsl:otherwise>
          </xsl:choose> 
     </xsl:for-each>
     <xsl:text>&#xa;&#xa;</xsl:text>
</xsl:template>
</xsl:stylesheet>